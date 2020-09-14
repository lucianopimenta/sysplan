using SysPlan.Core.Interface;
using SysPlan.Core.Repository;
using System;
using System.Collections.Generic;

namespace SysPlan.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        private Dictionary<Type, object> repositories;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!this.repositories.ContainsKey(type))
            {
                this.repositories[type] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)this.repositories[type];
        }

        public int Commit()
        {
            return this._dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        public IDbContext GetDbContext()
        {
            return this._dbContext;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dbContext != null)
                {
                    this._dbContext.Dispose();
                    this._dbContext = null;
                }
            }
        }
    }
}
