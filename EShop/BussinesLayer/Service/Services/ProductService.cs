﻿using BussinesLayer.Pagination;
using BussinesLayer.RequestModels.Product;
using BussinesLayer.ResponseModels.ApiResponseModel;
using BussinesLayer.Service.IServices;
using DataAccessLayer.UnitOfWork;
using DomainLayer.Entities;
using DomainLayer.RequestModels.Branch;
using DomainLayer.RequestModels.Category;
using DomainLayer.RequestModels.Product;
using DomainLayer.ResponseModel.Branch;
using DomainLayer.ResponseModel.Category;
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

        public async Task<ApiResponse> SearchProducts(string? name, string? category, string? branch, int pageIndex, int pageSize)
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

        public async Task<ApiResponse> AddBranch(BranchAddModel model)
        {
            var response = new ApiResponse();

            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                BranchName = model.BranchName,
            };

            await _unitOfWork.BranchRepository.AddAsync(branch);
            await _unitOfWork.SaveChangeAsync();

            response.SetOk(branch);

            return response;
        }

        public async Task<ApiResponse> AddCategory(CategoryAddModel model)
        {
            var response = new ApiResponse();

            var category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = model.CategoryName,
            };

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangeAsync();

            response.SetOk(category);

            return response;
        }

        public async Task<ApiResponse> GetCategories()
        {
            var response = new ApiResponse();

            var categoryEntities = await _unitOfWork.CategoryRepository.GetAllAsync(null);
            categoryEntities ??= new List<Category>();

            List<CategoryViewModel> categories = categoryEntities.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
            }).ToList();

            response.SetOk(categories);

            return response;
        }

        public async Task<ApiResponse> GetBranches()
        {
            var response = new ApiResponse();

            var branchEntities = await _unitOfWork.BranchRepository.GetAllAsync(null);
            branchEntities ??= new List<Branch>();

            List<BranchViewModel> branches = branchEntities.Select(x => new BranchViewModel
            {
                Id = x.Id,
                BranchName = x.BranchName,
            }).ToList();

            response.SetOk(branches);

            return response;
        }

        public async Task<ApiResponse> AddProduct(ProductAddModel model)
        {
            Product newProduct = new()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                DiscountPercentage = model.DiscountPercentage,
                Rating = model.Rating,
                Thumbnail = model.Thumbnail,
                CategoryId = model.CategoryId,
                BranchId = model.BranchId,
                Images = model.Images,
            };

            await _unitOfWork.ProductRepository.AddAsync(newProduct);
            await _unitOfWork.SaveChangeAsync();
            return new ApiResponse().SetOk("Created");
        }
    }
}
