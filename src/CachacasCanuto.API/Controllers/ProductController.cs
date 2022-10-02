using CachacasCanuto.API.Controllers.Shared;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace CachacasCanuto.API.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Busca os produtos com ou sem filtros
        /// </summary>
        /// <param name="name">Nome do produto</param>
        /// <param name="startContent">Valor inicial do intervalo de teor alcoólico</param>
        /// <param name="endContent">Valor final do intervalo de teor alcoólico</param>
        /// <response code="200">Produto encontrado com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] string? name, [FromQuery] decimal? startContent, [FromQuery] decimal? endContent)
        {
            var result = await _productService.GetAllProductsAsync(name, startContent, endContent);

            return CustomResponse(result);
        }

        /// <summary>
        /// Busca o produto pelo Id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <response code="200">Produto encontrado com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetProductByIdAsync(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            return CustomResponse(result);
        }
    }
}
