using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Implementation
{
    public class GenericDbContext<TDAO> : DbContext, IDbContext<TDAO> where TDAO : class
    {
        public GenericDbContext(DbContextOptions<GenericDbContext<TDAO>> options) : base(options)
        {
        }

        public DbSet<TDAO> Items { get; set; }

        public void BeginTransaction()
        {
            if (base.Database.CurrentTransaction != null)
            {
                base.Database.BeginTransaction();
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (base.Database.CurrentTransaction != null)
            {
                await base.Database.BeginTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            Task task = Task.Run(() => {
                Rollback();
            });

            await task;
        }

        public void Rollback()
        {
            if (base.Database.CurrentTransaction != null)
            {
                base.Database.RollbackTransaction();
            }
        }

        public void Commit()
        {
            base.SaveChanges();

            if (base.Database.CurrentTransaction != null)
            {
                base.Database.CommitTransaction();
            }
        }

        public async Task CommitAsync()
        {
            await base.SaveChangesAsync();

            if (base.Database.CurrentTransaction != null)
            {
                base.Database.CommitTransaction();
            } 
        }
    }
}
