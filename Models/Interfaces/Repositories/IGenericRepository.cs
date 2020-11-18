using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;

namespace HurtowniaReptiGood.Models.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByFieldAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> GetByIdAsync<TId>(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync<TId>(TId id);

    }
}