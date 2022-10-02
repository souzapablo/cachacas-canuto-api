using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces
{
    public interface IProductHttpService
    {
        Task<List<ExternalProductViewModel>?> GetAllProducts();
        Task<List<string>?> GetProductsIdsByName(string name);
        Task<ExternalProductViewModel?> GetProductById(string id);
    }
}
