using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces
{
    public interface IDbContext<TDAO>
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Rollback();
        Task RollbackAsync();
        void Commit();
        Task CommitAsync();
    }
}
