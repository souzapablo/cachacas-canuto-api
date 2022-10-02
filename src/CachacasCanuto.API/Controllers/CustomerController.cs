using CachacasCanuto.API.Controllers.Shared;
using CachacasCanuto.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CachacasCanuto.API.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Busca os clientes com ou sem filtros
        /// </summary>
        /// <param name="name">Nome a ser encontrado</param>
        /// <param name="startDate">Início do intervalo da data de nascimento</param>
        /// <param name="endDate">Fim do intervalo da data de nascimento</param>
        /// <response code="200">Cliente encontrado com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet]
        public async Task<IActionResult> GetCustomerByName([FromQuery] string? name, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _customerService.GetAllCustomersAsync(name, startDate, endDate);

            return CustomResponse(result);
        }

        /// <summary>
        /// Busca o cliente pelo Id
        /// </summary>
        /// <param name="id">Id do cliente para buscar</param>
        /// <response code="200">Cliente encontrado com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            return CustomResponse(result);
        }
    }
}
