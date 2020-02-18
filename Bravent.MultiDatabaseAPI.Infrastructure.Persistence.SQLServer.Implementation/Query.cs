using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Exceptions;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Implementation
{
    public class Query<TDAO, TID> : IQuery<TDAO, TID> where TDAO : class, IDAO<TID> 
    {
        private readonly GenericDbContext<TDAO> _context;

        public Query(IDbContext<TDAO> context)
        {
            _context = (GenericDbContext<TDAO>)context;
        }

        public DbSet<TDAO> GetDbSet()
        {
            return _context.Items;
        }

        public IEnumerable<TDAO> Find(Expression<Func<TDAO, bool>> func)
        {
            return GetDbSet().Where(func).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(Expression<Func<TDAO, bool>> func)
        {
            return await GetDbSet().Where(func).ToListAsync();
        }

        public IEnumerable<TDAO> Find(string sqlRaw)
        {
            return GetDbSet().FromSqlRaw(sqlRaw).ToList();
        }

        public async Task<IEnumerable<TDAO>> FindAsync(string sqlRaw)
        {
            return await GetDbSet().FromSqlRaw(sqlRaw).ToListAsync();
        }

        public TDAO Get(Expression<Func<TDAO, bool>> func)
        {
            TDAO dao = GetDbSet().Where(func).FirstOrDefault();

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public async Task<TDAO> GetAsync(Expression<Func<TDAO, bool>> func)
        {
            TDAO dao = await GetDbSet().Where(func).FirstOrDefaultAsync();

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public TDAO Get(TID id)
        {
            TDAO dao = GetDbSet().Find(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }

        public async Task<TDAO> GetAsync(TID id)
        {
            TDAO dao = await GetDbSet().FindAsync(id);

            if (dao == null)
            {
                throw new NotFoundException();
            }

            return dao;
        }
    }
}
