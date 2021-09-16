using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.Util
{
    public interface IUnitOfWork<TCtx> 
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<bool> CommitAsync();
    }
}
