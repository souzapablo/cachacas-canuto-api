using CachacasCanuto.Application.ViewModels.Products;

namespace CachacasCanuto.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>?> GetAllProductsAsync(string? name, decimal? startContent, decimal? endContent);
        Task<ProductViewModel?> GetProductByIdAsync(string id);
        Task<List<string>?> GetProductsIdsByNameAsync(string name);
    }
}
