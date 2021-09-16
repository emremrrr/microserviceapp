using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.Util
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
                                                Expression<Func<TEntity,
                                                bool>> filter = null,
                                                Expression<Func<TEntity, TEntity>> select = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                int pageSize = 0,
                                                int pageNumber = 0);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool showDeleted = false);

        Task Insert(TEntity entity);
        Task InsertRange(IEnumerable<TEntity> entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
