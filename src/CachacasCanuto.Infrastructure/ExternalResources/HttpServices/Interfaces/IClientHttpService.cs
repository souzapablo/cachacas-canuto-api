namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces
{
    public interface IClientHttpService
    {
        Task<string?> GetRequestAsync(string requestUri);
    }
}
