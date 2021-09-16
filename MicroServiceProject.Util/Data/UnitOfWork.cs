using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.Util
{
    public class UnitOfWork<TCtx> : IUnitOfWork<TCtx>, IDisposable where TCtx:DbContext
    {
        private readonly DbContext _context;
        private bool disposedValue;

        public UnitOfWork(TCtx context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }
        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
