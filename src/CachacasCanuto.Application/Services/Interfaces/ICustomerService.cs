using CachacasCanuto.Application.ViewModels.Customers;

namespace CachacasCanuto.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerViewModel>?> GetAllCustomersAsync(string? name, DateTime? startDate, DateTime? endDate);
        Task<CustomerViewModel?> GetCustomerByIdAsync(string id);
        Task<List<string>?> GetCustomersIdsByNameAsync(string name);
    }
}
