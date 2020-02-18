using System.Collections.Generic;
using System.Threading.Tasks;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Exceptions;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Implementation
{
    public class Persistence<TDAO, TID> : IPersistence<TDAO, TID> where TDAO : class, IDAO<TID>
    {
        private readonly GenericDbContext<TDAO> _context;

        public Persistence(IDbContext<TDAO> context)
        {
            _context = (GenericDbContext<TDAO>)context;
        }

        private void Remove(TDAO item)
        {
            _context.Items.Remove(item);
        }

        public DbSet<TDAO> GetDbSet()
        {
            return _context.Items;
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.BeginTransactionAsync();
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task CommitAsync()
        {
            await _context.CommitAsync();
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public async Task RollbackAsync()
        {
            await _context.RollbackAsync();
        }

        public void Delete(TDAO item)
        {
            Remove(item);
        }

        public async Task DeleteAsync(TDAO item)
        {
            Task task = Task.Run(() => {
                Remove(item);
            });

            await task;
        }

        public void Delete(TID id)
        {
            TDAO dao = _context.Items.Find(id);
            Remove(dao);
        }

        public async Task DeleteAsync(TID id)
        {
            TDAO dao = await _context.Items.FindAsync(id);
            Remove(dao);
        }

        public void Delete(IEnumerable<TDAO> items)
        {
            _context.Items.RemoveRange(items);
        }

        public async Task DeleteAsync(IEnumerable<TDAO> items)
        {
            Task task = Task.Run(() =>
            {
                Delete(items);
            });

            await task;
        }

        public TDAO Insert(TDAO item)
        {
            item = _context.Items.Add(item).Entity;

            return item;
        }

        public async Task<TDAO> InsertAsync(TDAO item)
        {   
            item = (await _context.Items.AddAsync(item)).Entity;

            return item;
        }

        public TDAO Update(TDAO item)
        {
            item = _context.Items.Update(item).Entity;

            return item;
        }

        public async Task<TDAO> UpdateAsync(TDAO item)
        {
            Task<TDAO> task = Task.Run(() =>
            {
                return Update(item);
            });
                
            return await task;
        }
    }
}
