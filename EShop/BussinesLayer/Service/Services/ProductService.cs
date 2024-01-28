using BussinesLayer.Pagination;
using BussinesLayer.RequestModels.Product;
using BussinesLayer.ResponseModels.ApiResponseModel;
using BussinesLayer.Service.IServices;
using DataAccessLayer.UnitOfWork;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> GetProductById(Guid productId)
        {
            var response = new ApiResponse();
            var product = await _unitOfWork.ProductRepository.GetProductById(productId);
            response.SetNotFound("Product ID - " + productId + " - Not Found.");

            if (product != null)
            {
                var productResponse = new ProductViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Rating = product.Rating,
                    BranchId = product.BranchId,
                    CategoryId = product.Category.Id,
                    BranchName = product.Branch.BranchName,
                    DiscountPercentage = product.DiscountPercentage,
                    CategoryName = product.Category.CategoryName,
                    Thumbnail = product.Thumbnail,
                    Stock = product.Stock,
                    Images = product.Images.Split(',').ToList(),
                };
                response.SetOk(productResponse);
            }
            return response;
        }

        public async Task<ApiResponse> GetProducts(string? name, string? category, string? branch, int pageIndex, int pageSize)
        {
            var listProductEntities = await _unitOfWork.ProductRepository.SearchProducts(name, category, branch);

            listProductEntities ??= new List<Product>();

            List<ProductViewModel> products = listProductEntities.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                BranchId = x.BranchId,
                CategoryId = x.CategoryId,
                BranchName = x.Branch.BranchName,
                DiscountPercentage = x.DiscountPercentage,
                CategoryName = x.Category.CategoryName,
                Thumbnail = x.Thumbnail,
                Stock = x.Stock,
                Rating = x.Rating,
                Images = x.Images.Split(',').ToList(),
            }).ToList();

            foreach (var product in products)
            {
                product.AvailableStock = await _unitOfWork.ProductRepository.GetProductAvailableStock(product.Id);
            }

            var paginatedProducts = products.Paginate(pageIndex, pageSize);

            return new ApiResponse().SetOk(paginatedProducts);
        }
    }
}
