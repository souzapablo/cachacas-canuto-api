using AutoMapper;
using CachacasCanuto.Application.Common.Interfaces;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Application.ViewModels.Products;
using CachacasCanuto.Application.ViewModels.Reports;
using CachacasCanuto.Application.ViewModels.Sales;
using CachacasCanuto.Core.Entities.Products;
using CachacasCanuto.Core.Entities.Reports;
using CachacasCanuto.Core.Entities.Sales;
using CachacasCanuto.Core.Helpers;
using CachacasCanuto.Core.Helpers.Pagination;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using System.Net;

namespace CachacasCanuto.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleHttpService _saleHttpService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IMessageHandler _message;
        private readonly IMapper _mapper;

        public SaleService(ISaleHttpService saleHttpService, ICustomerService customerService, IProductService productService, 
            IMessageHandler message, IMapper mapper)
        {
            _saleHttpService = saleHttpService;
            _customerService = customerService;
            _productService = productService;
            _message = message;
            _mapper = mapper;
        }

        public async Task<Pagination<SaleViewModel>?> GetSalesAsync(DateTime? startDate, DateTime? endDate, string? customerName, string? productName, int quantityPerPage, int currentPage)
        {
            var externalSalesViewModels = await _saleHttpService.GetAllSalesAsync();

            var sales = _mapper.Map<List<Sale>>(externalSalesViewModels);

            if (startDate.HasValue)
                sales = sales.Where(x => x.Date >= startDate).ToList();

            if (endDate.HasValue)
                sales = sales.Where(x => x.Date <= endDate).ToList();

            if (!string.IsNullOrEmpty(customerName))
            {
                var customersIds = await _customerService.GetCustomersIdsByNameAsync(customerName);

                if (customersIds is null)
                {
                    _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                    return null;
                }

                sales = sales.Where(x => customersIds.Contains(x.CustomerId)).ToList();
            }

            if (!string.IsNullOrEmpty(productName))
            {
                var productsIds = await _productService.GetProductsIdsByNameAsync(productName);

                if (productsIds is null)
                {
                    _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                    return null;
                }

                List<string> salesId = new();

                foreach (var sale in sales)
                {
                    if (sale.ItensContainId(productsIds))
                        salesId.Add(sale.Id);
                }

                sales = sales.Where(x => salesId.Contains(x.Id)).ToList();
            }

            var pagination = new Pagination<SaleViewModel>(sales.Count, currentPage, quantityPerPage);

            var salesViewModels = _mapper.Map<List<SaleViewModel>>(sales);

            pagination.AddData(salesViewModels);

            return pagination;
        }

        public async Task<List<CustomerSalesReportViewModel>?> GetSalesReportByCustomerAsnyc(DateTime? startDate, DateTime? endDate, int showItens)
        {
            List<CustomerSalesReport> reports = new();

            var customersViewModels = await _customerService.GetAllCustomersAsync(null, null, null);

            if (customersViewModels is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            var salesViewModels = await _saleHttpService.GetAllSalesAsync();

            if (salesViewModels is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            var productsViewModels = await _productService.GetAllProductsAsync(null, null, null);

            if (productsViewModels is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            if (startDate.HasValue)
                salesViewModels = salesViewModels?.Where(x => x.Date >= startDate.Value).ToList();

            if (endDate.HasValue)
                salesViewModels = salesViewModels?.Where(x => x.Date <= endDate.Value).ToList();

            var sales = _mapper.Map<List<Sale>>(salesViewModels);

            List<Sale> customerSales = new();

            foreach (var customer in customersViewModels)
            {
                customerSales = sales.Where(x => x.CustomerId == customer.Id).ToList();

                var totalAmount = customerSales.Sum(x => x.CalculatePrice());

                var mostSoldProducts = GetMostBoughtProducts(customerSales, showItens, productsViewModels);

                reports.Add(new(customer.Id, customer.Name, totalAmount, mostSoldProducts));
            }

            var reportsVm = _mapper.Map<List<CustomerSalesReportViewModel>>(reports);

            return reportsVm;
        }

        public async Task<List<ProductSalesReportViewModel>?> GetSalesReportByProductAsync(bool descending, OrderBy orderBy)
        {
            List<ProductSalesReportViewModel> reports = new();

            var salesViewModels = await _saleHttpService.GetAllSalesAsync();

            if (salesViewModels is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            var productsViewModels = await _productService.GetAllProductsAsync(null, null, null);

            if (productsViewModels is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            List<Sale> sales = _mapper.Map<List<Sale>>(salesViewModels);

            List<SaleItem> saleItens = new();

            foreach (var sale in sales)
                saleItens.AddRange(sale.Itens);

            foreach (var product in productsViewModels)
            {
                var productSales = saleItens.Where(x => x.Id == product.Id);
                var totalAmount = productSales.Sum(x => x.GetTotalAmount());

                reports.Add(new ProductSalesReportViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    QuantitySold = productSales.Count(),
                    TotalAmount = totalAmount
                });
            }

            if (descending)
                reports = OrderDescending(reports, orderBy);
            else
                reports = Order(reports, orderBy);

            return reports;
        }

        private List<ReportItem> GetMostBoughtProducts(List<Sale> sales, int amount, List<ProductViewModel> productsVm)
        {
            List<ReportItem> response = new();

            if (amount > productsVm.Count)
                amount = productsVm.Count;

            var products = _mapper.Map<List<Product>>(productsVm);

            Dictionary<string, int> salesPerItem = new();
            Dictionary<string, decimal> valuePerItem = new();

            foreach (var product in productsVm)
            {
                salesPerItem.Add(product.Id, 0);
                valuePerItem.Add(product.Id, 0);
            }

            List<SaleItem> saleItens = new();

            foreach (var sale in sales)
            {
                saleItens.AddRange(sale.Itens);
            }

            foreach (var item in saleItens)
            {
                salesPerItem[item.Id] += item.Quantity;
                valuePerItem[item.Id] += item.Quantity * item.UnitPrice;
            }

            var sorted = from entry in salesPerItem orderby entry.Value descending select entry;

            for (int i = 0; i < amount; i++)
            {
                var id = sorted.ElementAt(i).Key;

                var product = products.Single(x => x.Id == id);

                var report = new ReportItem(product, salesPerItem[product.Id], valuePerItem[product.Id]);

                response.Add(report);
            }

            return response;
        }

        private static List<ProductSalesReportViewModel> Order(List<ProductSalesReportViewModel> list, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.QuantitySold:
                    list = list.OrderBy(x => x.QuantitySold).ToList();
                    break;
                case OrderBy.TotalAmount:
                    list = list.OrderBy(x => x.TotalAmount).ToList();
                    break;
            }

            return list;
        }

        private static List<ProductSalesReportViewModel> OrderDescending(List<ProductSalesReportViewModel> list, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.QuantitySold:
                    list = list.OrderByDescending(x => x.QuantitySold).ToList();
                    break;
                case OrderBy.TotalAmount:
                    list = list.OrderByDescending(x => x.TotalAmount).ToList();
                    break;
            }

            return list;
        }
    }
}
