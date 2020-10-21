using HurtowniaReptiGood.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
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

            var list = await query.ToListAsync();

            return list;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new NullReferenceException($"Parameter {nameof(predicate)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            var result = query.Where(predicate);

            var list = await result.ToListAsync();

            return list;
        }
        public virtual async Task<TEntity> GetByFieldAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new NullReferenceException($"Parameter {nameof(predicate)} cannot be null.");

            var query = _context.Set<TEntity>().AsNoTracking();

            var result = await query.Where(predicate).FirstOrDefaultAsync();

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
                _context.Set<TEntity>().Update(entity);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
