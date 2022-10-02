using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices
{
    public class ClientHttpService : IClientHttpService
    {
        public HttpClient HttpClient { get; set; } = new();

        public async Task<string?> GetRequestAsync(string requestUri)
        {
            using var response = await HttpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
