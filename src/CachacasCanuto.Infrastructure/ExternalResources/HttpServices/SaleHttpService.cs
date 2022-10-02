using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class SaleHttpService : ISaleHttpService
    {
        private readonly IClientHttpService _httpClientService;
        private const string _url = "https://firebasestorage.googleapis.com/v0/b/testemonomytobackend/o/Vendas.json?alt=media";

        public SaleHttpService(IClientHttpService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<ExternalSaleViewModel>?> GetAllSalesAsync()
        {
            var response = await _httpClientService.GetRequestAsync(_url);

            if (response is null)
                return null;

            var sales = JsonConvert.DeserializeObject<List<ExternalSaleViewModel>>(response);

            return sales;
        }
    }
}
