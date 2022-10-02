using CachacasCanuto.API.Controllers.Shared;
using CachacasCanuto.Application.Common.Extensions;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CachacasCanuto.API.Controllers
{
    [ApiController]
    [Route("api/v1/sales")]
    public class SaleController : BaseController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Busca as vendas de maneira paginada
        /// </summary>
        /// <param name="quantityPerPage">Quantidade de itens por página</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="startDate">Início do intervalo da data de venda</param>
        /// <param name="endDate">Final do intervalo da data de nascimento</param>
        /// <param name="customerName">Nome do cliente</param>
        /// <param name="productName">Nome do produto</param>
        /// <response code="200">Vendas encontradas com sucesso</response>
        /// <response code="404">Vendas não encontradas</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSalesAsync([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? customerName, [FromQuery] string? productName, [FromQuery] int quantityPerPage = 10, [FromQuery] int currentPage = 1)
        {
            var result = await _saleService.GetSalesAsync(startDate, endDate, customerName, productName, quantityPerPage, currentPage);

            return CustomResponse(result);
        }

        /// <summary>
        /// Gerar relatório de vendas por cliente, incluindo produtos mais vendidos
        /// </summary>
        /// <param name="startDate">Início do intervalo da data de venda</param>
        /// <param name="endDate">Final do intervalo da data de venda</param>
        /// <param name="showItens">Quantidade de itens a serem exibidos (padrão = 1)</param>
        /// <response code="200">Vendas encontradas com sucesso</response>
        /// <response code="404">Vendas não encontradas</response>
        [HttpGet("report/customers")]
        public async Task<IActionResult> GetSalesReportByCustomerAsync([FromQuery] DateTime? startDate, DateTime? endDate, int showItens = 1)
        {
            var result = await _saleService.GetSalesReportByCustomerAsnyc(startDate, endDate, showItens);

            return CustomResponse(result);
        }

        /// <summary>
        /// Gerar planilha contendo o relatório de vendas por cliente, incluindo produtos mais vendidos
        /// </summary>
        /// <param name="startDate">Início do intervalo da data de venda</param>
        /// <param name="endDate">Final do intervalo da data de venda</param>
        /// <param name="showItens">Quantidade de itens a serem exibidos (padrão = 1)</param>
        /// <response code="200">Relatório gerado com sucesso</response>
        /// <response code="404">Relatório não gerado</response>
        [HttpGet("report/customers/file")]
        public async Task<IActionResult> GetSalesReportByCustomerFileAsync([FromQuery] DateTime? startDate, DateTime? endDate, int showItens = 1)
        {
            var result = await _saleService.GetSalesReportByCustomerAsnyc(startDate, endDate, showItens);

            if (result is null)
                return CustomResponse(result);

            var file = GenerateFileExtension.GenerateCustomerReportFile(result);

            return File(file.Content, file.FileReportType, file.FileReportName);
        }

        /// <summary>
        /// Gerar relatório de vendas por produto
        /// </summary>
        /// <param name="descending">False = ordernar de forma crescente e true = ordenar de forma descrescente</param>
        /// <param name="orderBy">1 = ordernar por quantidade vendida, 2 = ordenar por total vendido em reais </param>
        /// <response code="200">Relatório gerado com sucesso</response>
        /// <response code="404">Relatório não gerado</response>
        [HttpGet("report/products")]
        public async Task<IActionResult> GetSalesReportByProductsAsync([FromQuery] bool descending, [FromQuery] OrderBy orderBy)
        {
            var result = await _saleService.GetSalesReportByProductAsync(descending, orderBy);

            return CustomResponse(result);
        }

        /// <summary>
        /// Gerar planilha contendo o relatório de vendas por cliente, incluindo produtos mais vendidos
        /// </summary>
        /// <param name="descending">False = ordernar de forma crescente e true = ordenar de forma descrescente</param>
        /// <param name="orderBy">1 = ordernar por quantidade vendida, 2 = ordenar por total vendido em reais </param>
        /// <response code="200">Relatório gerado com sucesso</response>
        /// <response code="404">Relatório não gerado</response>
        [HttpGet("report/products/file")]
        public async Task<IActionResult> GetSalesReportByProductsFileAsync([FromQuery] bool descending, [FromQuery] OrderBy orderBy)
        {
            var result = await _saleService.GetSalesReportByProductAsync(descending, orderBy);

            if (result is null)
                return CustomResponse(result);

            var file = GenerateFileExtension.GenerateProductReportFile(result);

            return File(file.Content, file.FileReportType, file.FileReportName);
        }
    }
}
