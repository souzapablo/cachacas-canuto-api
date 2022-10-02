using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces
{
    public interface ISaleHttpService
    {
        Task<List<ExternalSaleViewModel>?> GetAllSalesAsync();
    }
}
