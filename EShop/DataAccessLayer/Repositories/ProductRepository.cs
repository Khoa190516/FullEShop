using ApplicationContextLayer;
using DataAccessLayer.IRepositories;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetListProductWithFullInfoByIds(List<Guid> listProductIds)
        {
            IQueryable<Product> query = _dbSet;

            var products = await query
                .Where(product => listProductIds
                .Contains(product.Id))
                .Include(product => product.Branch)
                .Include(product => product.Category)
                .ToListAsync();

            return products ??= new();
        }

        public async Task<int> GetProductAvailableStock(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if(product == null)
            {
                return -1;
            }

            int? totalBoughtQuantity = await _context.CartProducts
                .Where(x => x.ProductId == productId)
                .SumAsync(x => x.Quantity);

            if(totalBoughtQuantity == null)
            {
                return -1;
            }

            return product.Stock - totalBoughtQuantity.Value;
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _dbSet.Where(p => p.Id == productId).Include(p => p.Branch).Include(p => p.Category).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> SearchProducts(string? name, string? category, string? branch)
        {
            IQueryable<Product> query = _dbSet;
            List<Product> products = new();

            if(!string.IsNullOrEmpty(name))
            {
                query = query
                    .Where(x => x.Title.ToLower().Contains(name.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(branch))
            {
                query = query
                    .Where(x => x.Branch.BranchName.ToLower().Contains(branch.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query
                    .Where(x => x.Category.CategoryName.ToLower().Contains(category.ToLower().Trim()));
            }

            var result = await query
                .Include(c => c.Branch)
                .Include(c => c.Category)
                .ToListAsync();

            return result;
        }
    }
}
