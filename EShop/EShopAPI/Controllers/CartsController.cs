
using BussinesLayer.RequestModels.CartProduct;
using BussinesLayer.ResponseModels.ApiResponseModel;
using BussinesLayer.Service.IServices;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetCartByUser()
        {
            var identityClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (identityClaim is null) return Unauthorized();

            var isGuidId = Guid.TryParse(identityClaim.Value, out Guid userId);

            if (!isGuidId)
            {
                return BadRequest();
            }

            var response = await _cartService.GetCartByUserId(userId);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpPut()]
        public async Task<IActionResult> UpdateProductToCart([FromBody] CartProductUpdateModel productUpdate)
        {
            var response = new ApiResponse();
            var identityClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (identityClaim is null) return Unauthorized();

            var isGuidId = Guid.TryParse(identityClaim.Value, out Guid userId);

            if (!isGuidId)
            {
                return BadRequest();
            }

            switch (productUpdate.IsDecrease)
            {
                case false:
                    {
                        response = await _cartService.UpdateProductToCart(userId, productUpdate);
                        break;
                    }
                case true:
                    {
                        response = await _cartService.DecreaseProductFromCart(userId, productUpdate);
                        break;
                    }
            }
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(Guid productId)
        {
            var identityClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (identityClaim is null) return Unauthorized();

            var isGuidId = Guid.TryParse(identityClaim.Value, out Guid userId);

            if (!isGuidId)
            {
                return BadRequest();
            }

            var response = await _cartService.RemoveProductFromCart(userId, productId);
            if (response == null) return BadRequest();
            return Ok(response);
        }
    }
}
