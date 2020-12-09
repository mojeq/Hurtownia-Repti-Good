using EFCoreSecondLevelCacheInterceptor;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository, IGenericRepository<TEntity> where TEntity : class
    {
        public GenericRepository(MyContext context) : base(context) { }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = _context.Set<TEntity>().AsNoTracking();

            var list = await query.Cacheable().ToListAsync();

            return list;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null) throw new NullReferenceException($"Parameter {nameof(predicate)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            query = include?.Invoke(query) ?? query;

            var result = query.Where(predicate);

            var list = await result.Cacheable().ToListAsync();

            return list;
        }

        public virtual async Task<TEntity> GetByFieldAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null) throw new NullReferenceException($"Parameter {nameof(predicate)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            query = include?.Invoke(query) ?? query;

            query = query.Where(predicate);

            var result = await query.Cacheable().FirstOrDefaultAsync();

            return result;
        }

        public virtual async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null) throw new NullReferenceException($"Parameter {nameof(predicate)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            query = include?.Invoke(query) ?? query;

            var result = await query.Cacheable().SingleOrDefaultAsync(predicate);

            return result;
        }

        public virtual async Task<TEntity> GetByIdAsync<TId>(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (id == null) throw new NullReferenceException($"Parameter {nameof(id)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            query = include?.Invoke(query) ?? query;

            var lambda = CreateFindByPrimaryKeyLambda(id);

            var result = await query.Cacheable().SingleOrDefaultAsync(lambda);

            return result;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null) throw new NullReferenceException($"Parameter {nameof(entity)} cannot be null.");

            try
            {
                var result = await _context.Set<TEntity>().AddAsync(entity);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }

            return entity;
        }

        public virtual async Task Update(TEntity entity)
        {
            if (entity == null) throw new NullReferenceException($"Parameter {nameof(entity)} cannot be null.");

            try
            {
                var result  = _context.Set<TEntity>().Update(entity);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new NullReferenceException($"Parameter {nameof(entities)} cannot be null.");

            try
            {
                _context.Set<TEntity>().UpdateRange(entities);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new NullReferenceException($"Parameter {nameof(entity)} cannot be null.");

            _context.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync<TId>(TId id)
        {
            if (id == null) throw new NullReferenceException($"Parameter {nameof(id)} cannot be null.");

            var entity = await GetByIdAsync(id);

            _context.Remove(entity);

            await _context.SaveChangesAsync();
        }

        protected Expression<Func<TEntity, bool>> CreateFindByPrimaryKeyLambda<TId>(TId id)
        {
            var pkPropertyName = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            var propertyInfo = typeof(TEntity).GetProperty(pkPropertyName);

            if (propertyInfo?.PropertyType != typeof(TId)) throw new Exception( $"Invalid type of {nameof(id)} argument.");

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var member = Expression.MakeMemberAccess(parameter, propertyInfo);
            var constant = Expression.Constant(id, id.GetType());
            var equation = Expression.Equal(member, constant);

            return Expression.Lambda<Func<TEntity, bool>>(equation, parameter);
        }
    }
}
