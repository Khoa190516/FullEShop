using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProducts(string? name, string? category, string? branch);

        Task<int> GetProductAvailableStock(Guid productId);

        Task<Product?> GetProductById(Guid productId);

        Task<List<Product>> GetListProductWithFullInfoByIds(List<Guid> listProductIds);
    }
}
