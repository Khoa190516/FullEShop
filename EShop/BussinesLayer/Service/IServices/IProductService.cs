using BussinesLayer.RequestModels.Product;
using BussinesLayer.ResponseModels.ApiResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Service.IServices
{
    public interface IProductService
    {
        Task<ApiResponse> GetProductById(Guid productId);

        Task<ApiResponse> GetProducts(string? name, string? category, string? branch, int pageIndex, int pageSize);
    }
}
