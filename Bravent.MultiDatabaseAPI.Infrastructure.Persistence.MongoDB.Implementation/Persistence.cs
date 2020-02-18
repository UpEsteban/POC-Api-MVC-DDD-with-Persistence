using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation
{
    public class Persistence<TDAO, TID> : BaseDb<TDAO, TID>, IPersistence<TDAO, TID> where TDAO : class, IDAO<TID>, new()
    {
        public Persistence(IDbContext<TDAO> context) : base(context)
        {
            
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
            _context.GetMongoCollection().FindOneAndDelete(GetIdFilter(item.GetId()));
        }

        public async Task DeleteAsync(TDAO item)
        {   
            await _context.GetMongoCollection().DeleteOneAsync(GetIdFilter(item.GetId()));
        }

        public void Delete(IEnumerable<TDAO> items)
        {
            _context.GetMongoCollection().DeleteMany(GetIdFilter(items.Select(i => i.GetId())));
        }

        public async Task DeleteAsync(IEnumerable<TDAO> items)
        {
            await _context.GetMongoCollection().DeleteManyAsync(GetIdFilter(items.Select(i => i.GetId())));
        }

        public void Delete(TID id)
        {
            _context.GetMongoCollection().FindOneAndDelete(GetIdFilter(id));
        }

        public async Task DeleteAsync(TID id)
        {
            await _context.GetMongoCollection().FindOneAndDeleteAsync(GetIdFilter(id));
        }

        public TDAO Insert(TDAO item)
        {
            _context.GetMongoCollection().InsertOne(item);
            return item;
        }

        public async Task<TDAO> InsertAsync(TDAO item)
        {
            await _context.GetMongoCollection().InsertOneAsync(item);
            return item;
        }

        public TDAO Update(TDAO item)
        {
            _context.GetMongoCollection().FindOneAndReplace(GetIdFilter(item.GetId()), item);
            return item;
        }

        public async Task<TDAO> UpdateAsync(TDAO item)
        {
            await _context.GetMongoCollection().FindOneAndReplaceAsync(GetIdFilter(item.GetId()), item);
            return item;
        }
    }
}
