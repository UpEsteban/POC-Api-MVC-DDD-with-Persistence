using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation
{
    public class BaseDb<TDAO, TID> where TDAO : class
    {
        protected readonly GenericDbContext<TDAO> _context;

        public BaseDb(IDbContext<TDAO> context)
        {
            _context = (GenericDbContext<TDAO>)context;
        }

        protected FilterDefinition<TDAO> GetIdFilter(TID id)
        {
            return GetFilter().Eq("_id", new ObjectId(id.ToString()));
        }

        protected FilterDefinition<TDAO> GetIdFilter(IEnumerable<TID> ids)
        {
            return GetFilter().In("_id", ids.Select(id => new ObjectId(id.ToString())));
        }

        internal FilterDefinitionBuilder<TDAO> GetFilter()
        {
            return Builders<TDAO>.Filter;
        }
    }
}
