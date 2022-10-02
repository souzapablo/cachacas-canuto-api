using CachacasCanuto.Infrastructure.ExternalResources.ViewModels;

namespace CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces
{
    public interface ICustomerHttpService
    {
        Task<List<ExternalCustomerViewModel>?> GetAllCustomers();
        Task<List<string>?> GetCustomersIdsByName(string name);
        Task<ExternalCustomerViewModel?> GetCustomerById(string id);
    }
}
