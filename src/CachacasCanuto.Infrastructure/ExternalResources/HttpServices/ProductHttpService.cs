using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {
        private readonly IClientHttpService _httpClientService;
        private const string _url = "https://firebasestorage.googleapis.com/v0/b/testemonomytobackend/o/Catalogo.json?alt=media";

        public ProductHttpService(IClientHttpService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<ExternalProductViewModel>?> GetAllProducts()
        {
            var response = await _httpClientService.GetRequestAsync(_url);

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
