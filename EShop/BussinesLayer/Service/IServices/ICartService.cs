using BussinesLayer.RequestModels.CartProduct;
using BussinesLayer.ResponseModels.ApiResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Service.IServices
{
    public interface ICartService
    {
        Task<ApiResponse> GetCartByUserId(Guid userId);
        Task<ApiResponse> UpdateProductToCart(Guid userId, CartProductUpdateModel productAdd);
        Task<ApiResponse> DecreaseProductFromCart(Guid userId, CartProductUpdateModel productAdd);
        Task<ApiResponse> RemoveProductFromCart(Guid userId, Guid productId);
    }
}
