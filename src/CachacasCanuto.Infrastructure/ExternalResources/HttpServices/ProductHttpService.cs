using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.Options;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {
        private readonly IClientHttpService _httpClientService;
        private readonly ExternalResourcesOptions _options;

        public ProductHttpService(IClientHttpService httpClientService, IOptions<ExternalResourcesOptions> options)
        {
            _httpClientService = httpClientService;
            _options = options.Value;
        }

        public async Task<List<ExternalProductViewModel>?> GetAllProducts()
        {
            var response = await _httpClientService.GetRequestAsync(_options.ExternalUrl + "Catalogo.json?alt=media");

            if (response is null)
                return null;

            response = Regex.Replace(response, "Classificaao", "Classificacao");

            var products = JsonConvert.DeserializeObject<List<ExternalProductViewModel>>(response);

            return products;
        }

        public async Task<ExternalProductViewModel?> GetProductById(string id)
        {
            var prducts = await GetAllProducts();

            if (prducts is null)
                return null;

            var product = prducts.FirstOrDefault(x => x.Id == id);

            return product;
        }

        public async Task<List<string>?> GetProductsIdsByName(string name)
        {
            var products = await GetAllProducts();

            if (products is null)
                return null;

            products = products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            var ids = products.Select(x => x.Id).ToList();

            return ids;
        }
    }
}
