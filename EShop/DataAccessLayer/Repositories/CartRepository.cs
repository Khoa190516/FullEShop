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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Cart?> GetCartWithProductIdsByUserId(Guid userId)
        {
            IQueryable<Cart> query = _dbSet;
            var cartEntity = await query
                .Where(cart => cart.Customer.Id == userId)
                .Include(cart => cart.Customer)
                .Include(cart => cart.CartProducts)
                .FirstOrDefaultAsync();
            return cartEntity;
        }
    }
}
