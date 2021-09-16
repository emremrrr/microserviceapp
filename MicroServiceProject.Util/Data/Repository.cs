using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.Util
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private IQueryable<TEntity> _query;

        public Repository(DbContext context)
        {
            _context = context;
            _query = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
       Expression<Func<TEntity,
       bool>> filter = null,
       Expression<Func<TEntity, TEntity>> select = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
       int pageSize = 0,
       int pageNumber = 0)
        {
            if (filter != null)
                _query = _query.Where(filter);
            if (select != null)
                _query = _query.Select(select);

            else if (orderBy != null)
                _query = orderBy(_query);

            if (pageSize > 0 && pageNumber > 0)
                _query = _query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (include != null)
                _query = include(_query);

            return await _query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool showDeleted = false)
        {
            if (filter != null)
                _query = _query.Where(filter);
            if (select != null)
                _query = _query.Select(select);
            if (orderBy != null)
                _query = orderBy(_query);
            if (include != null)
                _query = include(_query);

            return await _query.FirstOrDefaultAsync();
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            await Update(entity);
        }

        public async Task InsertRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().AddRange(entity);
            await Task.CompletedTask;
        }
    }
}
