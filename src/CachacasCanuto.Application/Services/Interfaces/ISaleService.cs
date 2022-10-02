using CachacasCanuto.Application.ViewModels.Reports;
using CachacasCanuto.Application.ViewModels.Sales;
using CachacasCanuto.Core.Helpers;
using CachacasCanuto.Core.Helpers.Pagination;

namespace CachacasCanuto.Application.Services.Interfaces
{
    public interface ISaleService 
    {
        Task<Pagination<SaleViewModel>?> GetSalesAsync(DateTime? startDate, DateTime? endDate, string? customerName, string? productName, int quantityPerPage, int currentPage);
        Task<List<CustomerSalesReportViewModel>?> GetSalesReportByCustomerAsnyc(DateTime? startDate, DateTime? endDate, int showItens);
        Task<List<ProductSalesReportViewModel>?> GetSalesReportByProductAsync(bool descending, OrderBy orderBy);
    }
}
