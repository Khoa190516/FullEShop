﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task DeleteByIdAsync(Guid id);
        Task<bool> IsExisted(Guid id);
    }
}
