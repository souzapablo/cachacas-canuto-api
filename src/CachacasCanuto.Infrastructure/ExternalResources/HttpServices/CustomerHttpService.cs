using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class CustomerHttpService : ICustomerHttpService
    {
        private readonly IClientHttpService _httpClientService;
        private const string _url = "https://firebasestorage.googleapis.com/v0/b/testemonomytobackend/o/Clientes.json?alt=media";

        public CustomerHttpService(IClientHttpService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<ExternalCustomerViewModel>?> GetAllCustomers()
        {
            var response = await _httpClientService.GetRequestAsync(_url);

            if (response is null)
                return null;

            response = Regex.Replace(response, "(\\d{2});", "$1");
            response = Regex.Replace(response, "Nomee", "Nome");
            response = Regex.Replace(response, "Nnome", "Nome");
            response = Regex.Replace(response, "(\"DatNasc\")", "DataNascimento");
            response = Regex.Replace(response, "(\"DataNasc\")", "DataNascimento");
            response = Regex.Replace(response, "(\"DataNascmento\")", "DataNascimento");
            response = Regex.Replace(response, "(\\d{2})/(\\d{2})/(\\d{4})", "$3-$2-$1");
            response = Regex.Replace(response, "(\\d{2})/(\\d{1})/(\\d{4})", "$3-0$2-$1");

            var clients = JsonConvert.DeserializeObject<List<ExternalCustomerViewModel>>(response);

            return clients;
        }

        public async Task<List<string>?> GetCustomersIdsByName(string name)
        {
            var customers = await GetAllCustomers();

            if (customers is null)
                return null;

            customers = customers.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            return customers.Select(x => x.Id).ToList();
        }

        public async Task<ExternalCustomerViewModel?> GetCustomerById(string id)
        {
            var customers = await GetAllCustomers();

            if (customers is null)
                return null;

            var customer = customers.FirstOrDefault(x => x.Id == id);

            if (customer is null)
                return null;

            return customer;
        }
    }
}
