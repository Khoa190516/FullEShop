using BussinesLayer.RequestModels.Product;
using BussinesLayer.ResponseModels.ApiResponseModel;
using DomainLayer.RequestModels.Branch;
using DomainLayer.RequestModels.Category;
using DomainLayer.RequestModels.Product;
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

        Task<ApiResponse> SearchProducts(string? name, string? category, string? branch, int pageIndex, int pageSize);

        Task<ApiResponse> AddBranch(BranchAddModel model);

        Task<ApiResponse> AddCategory(CategoryAddModel model);

        Task<ApiResponse> GetCategories();

        Task<ApiResponse> GetBranches();

        Task<ApiResponse> AddProduct(ProductAddModel model);
    }
}
