using Newtonsoft.Json;

namespace CachacasCanuto.Infrastructure.ExternalResources.ViewModels
{
    public class ExternalCustomerViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; } = null!;

        [JsonProperty(PropertyName = "Nome")]
        public string Name { get; set; } = null!;

        [JsonProperty(PropertyName = "DataNascimento")]
        public DateTime BirthDate { get; set; }
    }
}
