using System.Reflection;
using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;
using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources
{
    public static class ReadJsonExtension
    {
        public static string ReadProductJson()
        {
            using StreamReader r = new("./../CachacasCanuto.Core/Data/Catalogo.json");

            string json = r.ReadToEnd();

            return json;
        }

        public static string ReadCustomersJson()
        {
            using StreamReader r = new("./../CachacasCanuto.Core/Data/Clientes.json");

            string json = r.ReadToEnd();

            return json;
        }

        public static List<ExternalSaleViewModel>? ReadSalesJson()
        {
            using StreamReader r = new("./../CachacasCanuto.Core/Data/Vendas.json");

            string json = r.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<ExternalSaleViewModel>>(json);

            return list;
        }
    }
}