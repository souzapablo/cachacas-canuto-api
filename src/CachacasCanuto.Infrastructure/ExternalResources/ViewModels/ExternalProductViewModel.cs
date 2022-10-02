using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources.ViewModels
{
    public class ExternalProductViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; } = null!;

        [JsonProperty(PropertyName = "Marca")]
        public string Label { get; set; } = null!;

        [JsonProperty(PropertyName = "Classificacao")]
        public string Classification { get; set; } = null!;

        [JsonProperty(PropertyName = "Nome")]
        public string Name { get; set; } = null!;

        [JsonProperty(PropertyName = "TeorAlcoolico")]
        public decimal AlcooholContent { get; set; }

        [JsonProperty(PropertyName = "Regiao")]
        public string Region { get; set; } = null!;

        [JsonProperty(PropertyName = "PrecoAtual")]
        public decimal Price { get; set; }
    }
}
