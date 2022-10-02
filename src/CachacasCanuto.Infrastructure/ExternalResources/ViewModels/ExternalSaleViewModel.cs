using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources.ViewModels
{
    public class ExternalSaleViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; } = null!;

        [JsonProperty(PropertyName = "IdCliente")]
        public string CustomerId { get; set; } = null!;

        [JsonProperty(PropertyName = "Data")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "Itens")]
        public List<ExternalSaleItemViewModel> Itens { get; set; } = new();
    }

    public class ExternalSaleItemViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; } = null!;

        [JsonProperty(PropertyName = "PrecoUnitario")]
        public decimal UnitPrice { get; set; }

        [JsonProperty(PropertyName = "Quantidade")]
        public int Quantity { get; set; }
    }
}
