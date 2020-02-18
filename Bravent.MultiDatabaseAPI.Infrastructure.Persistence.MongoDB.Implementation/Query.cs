using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation
{
    public class Query<TDAO,TID> : BaseDb<TDAO, TID>, IQuery<TDAO, TID> where TDAO : class, IDAO<TID>, new()
    {
        public Query(IDbContext<TDAO> context) : base(context)
        {
        }

        public IEnumerable<TDAO> Find(FilterDefinition<TDAO> filter)
        {
            return _context.GetMongoCollection().Find(filter).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(FilterDefinition<TDAO> filter)
        {
            return await _context.GetMongoCollection().Find(filter).ToListAsync();
        }

        public IEnumerable<TDAO> Find(Expression<Func<TDAO, bool>> func)
        {
            return _context.GetMongoCollection().Find(func).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(Expression<Func<TDAO, bool>> func)
        {
            return await _context.GetMongoCollection().Find(func).ToListAsync();
        }

        public TDAO Get(FilterDefinition<TDAO> filter)
        {
            return _context.GetMongoCollection().Find(filter).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(FilterDefinition<TDAO> filter)
        {
            return await _context.GetMongoCollection().Find(filter).FirstOrDefaultAsync();
        }

        public TDAO Get(Expression<Func<TDAO, bool>> func)
        {
            return _context.GetMongoCollection().Find(func).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(Expression<Func<TDAO, bool>> func)
        {
            return await _context.GetMongoCollection().Find(func).FirstOrDefaultAsync();
        }

        public TDAO Get(TID id)
        {
            return _context.GetMongoCollection().Find(GetIdFilter(id)).FirstOrDefault();
        }

        public async Task<TDAO> GetAsync(TID id)
        {
            return await _context.GetMongoCollection().Find(GetIdFilter(id)).FirstOrDefaultAsync();
        }
    }
}
