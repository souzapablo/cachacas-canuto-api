using System.Reflection;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Newtonsoft.Json;

namespace CachacasCanuto.Application.Common
{
    public static class ReadJsonExtension
    {
        public static string ReadProductJson()
        {
            using StreamReader r = new StreamReader("./../CachacasCanuto.Infrastructure/Data/Catalogo.json");
            
            string json = r.ReadToEnd();

            return json;
        }

        public static string ReadCustomersJson()
        {
            using StreamReader r = new StreamReader("./../CachacasCanuto.Infrastructure/Data/Clientes.json");
            
            string json = r.ReadToEnd();

            return json;
        }

        public static List<ExternalSaleViewModel>? ReadSalesJson()
        {
            using StreamReader r = new StreamReader("./../CachacasCanuto.Infrastructure/Data/Vendas.json");
            
            string json = r.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<ExternalSaleViewModel>>(json);

            return list;
        }
    }
}