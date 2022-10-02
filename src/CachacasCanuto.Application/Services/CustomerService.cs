using AutoMapper;
using CachacasCanuto.Application.Common.Interfaces;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Application.ViewModels.Customers;
using CachacasCanuto.Core.Helpers;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using System.Net;

namespace CachacasCanuto.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerHttpService _customerHttpService;
        private readonly IMessageHandler _message;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerHttpService customerHttpService, IMessageHandler message, IMapper mapper)
        {
            _customerHttpService = customerHttpService;
            _mapper = mapper;
            _message = message;
        }

        public async Task<List<CustomerViewModel>?> GetAllCustomersAsync(string? name, DateTime? startDate, DateTime? endDate)
        {
            var customers = await _customerHttpService.GetAllCustomers();

            if (customers is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            if (!string.IsNullOrWhiteSpace(name))
                customers = customers?.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            if (startDate.HasValue)
                customers = customers?.Where(x => x.BirthDate >= startDate.Value).ToList();

            if (endDate.HasValue)
                customers = customers?.Where(x => x.BirthDate <= endDate.Value).ToList();

            var customersVm = _mapper.Map<List<CustomerViewModel>>(customers);

            return customersVm;
        }

        public async Task<CustomerViewModel?> GetCustomerByIdAsync(string id)
        {
            var customer = await _customerHttpService.GetCustomerById(id);

            if (customer is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, $"Cliente com o Id {id} não encontrado."));
                return null;
            }

            var customerVm = _mapper.Map<CustomerViewModel>(customer);

            return customerVm;
        }

        public async Task<List<string>?> GetCustomersIdsByNameAsync(string name)
        {
            var customersIds = await _customerHttpService.GetCustomersIdsByName(name);

            if (customersIds is null)
            {
                _message.AddMessage(new ErrorMessage(HttpStatusCode.NotFound, "Não foi possível conectar com o servidor."));
                return null;
            }

            return customersIds;
        }
    }
}
