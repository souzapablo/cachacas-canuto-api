using CachacasCanuto.Application.Common;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.Options;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class SaleHttpService : ISaleHttpService
    {
        private readonly IClientHttpService _httpClientService;
        private readonly ExternalResourcesOptions _options;
        public SaleHttpService(IClientHttpService httpClientService, IOptions<ExternalResourcesOptions> options)
        {
            _httpClientService = httpClientService;
            _options = options.Value;
        }

        public async Task<List<ExternalSaleViewModel>?> GetAllSalesAsync()
        {
            var response = await _httpClientService.GetRequestAsync(_options.ExternalUrl + "Vendas.json?alt=media");

            if (response is null)
                return ReadJsonExtension.ReadSalesJson();

            var sales = JsonConvert.DeserializeObject<List<ExternalSaleViewModel>>(response);

            return sales;
        }
    }
}
