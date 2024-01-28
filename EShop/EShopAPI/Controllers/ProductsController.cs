using BussinesLayer.Service.IServices;
using DomainLayer.RequestModels.Branch;
using DomainLayer.RequestModels.Category;
using DomainLayer.RequestModels.Product;
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
            var response = await _productService.SearchProducts(name, category, branch, pageIndex, pageSize);

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

        [AllowAnonymous]
        [HttpPost("branch")]
        public async Task<IActionResult> AddBranch([FromBody] BranchAddModel model)
        {
            var response = await _productService.AddBranch(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryAddModel model)
        {
            var response = await _productService.AddCategory(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productService.GetCategories();

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("branches")]
        public async Task<IActionResult> GetBranches()
        {
            var response = await _productService.GetBranches();

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddModel model)
        {
            var response = await _productService.AddProduct(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

    }
}
