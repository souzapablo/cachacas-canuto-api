using AutoMapper;
using CachacasCanuto.Application.Common.Interfaces;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Application.ViewModels.Products;
using CachacasCanuto.Core.Helpers;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using System.Net;

namespace CachacasCanuto.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductHttpService _productHttpService;
        private readonly IMessageHandler _message;
        private readonly IMapper _mapper;
        public ProductService(IProductHttpService productHttpService, IMessageHandler message, IMapper mapper)
        {
            _productHttpService = productHttpService;
            _message = message;
            _mapper = mapper;
        }

        public async Task<List<ProductViewModel>?> GetAllProductsAsync(string? name, decimal? startContent = null, decimal? endContent = null)
        {
            var products = await _productHttpService.GetAllProducts();

            if (products is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            if (startContent.HasValue)
                products = products.Where(x => x.AlcooholContent >= startContent).ToList();

            if (endContent.HasValue)
                products = products.Where(x => x.AlcooholContent <= endContent).ToList();

            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return productsViewModels;
        }

        public async Task<ProductViewModel?> GetProductByIdAsync(string id)
        {
            var product = await _productHttpService.GetProductById(id);

            if (product is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, $"Produto com o Id {id} não encontrado."));
                return null;
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);

            return productViewModel;
        }

        public async Task<List<string>?> GetProductsIdsByNameAsync(string name)
        {
            var productsIds = await _productHttpService.GetProductsIdsByName(name);

            if (productsIds is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            return productsIds;
        }
    }
}
