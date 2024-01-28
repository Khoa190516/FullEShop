using BussinesLayer.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] string? name, 
            [FromQuery] string? category,
            [FromQuery] string? branch,
            [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 5)
        {
            var response = await _productService.GetProducts(name, category, branch, pageIndex, pageSize);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("{productId:Guid}")]
        public async Task<IActionResult> GetProductById(
            Guid productId)
        {
            var response = await _productService.GetProductById(productId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
