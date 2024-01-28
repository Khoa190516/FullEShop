using ApplicationContextLayer;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context; 

        public ICustomerRepository CustomerRepository { get; }

        public IBranchRepository BranchRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public ICartRepository CartRepository { get; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            CustomerRepository = new CustomerRepository(_context);
            BranchRepository = new BranchRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            CartRepository = new CartRepository(_context);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
