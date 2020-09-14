using SysPlan.Core.Interface;
using SysPlan.Core.Repository;
using System;

namespace SysPlan.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int Commit();
        IDbContext GetDbContext();
    }
}
