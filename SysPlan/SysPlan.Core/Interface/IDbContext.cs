using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace SysPlan.Core.Interface
{
    public interface IDbContext
    {
        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

        void Dispose();

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
        DbContext Ctx();
    }
}
