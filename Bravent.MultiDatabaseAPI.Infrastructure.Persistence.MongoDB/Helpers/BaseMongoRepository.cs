using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers
{
    public class BaseMongoRepository<TDAO, TID> where TDAO : BaseDAO, IMongoDAO<TID>
    {
        private readonly string _tableName;

        public BaseMongoRepository(string tableName)
        {
            _tableName = tableName;
        }

        protected string GetTableName()
        {
            return _tableName;
        }

        protected Expression<Func<TDAO, bool>> GetExpression(Expression<Func<TDAO, bool>> func)
        {
            Expression<Func<BaseDAO, bool>> tableExpr = item => item.TableName == GetTableName();

            return Expression.Lambda<Func<TDAO, bool>>(Expression.AndAlso(func.Body, tableExpr.Body), func.Parameters[0]);
        }

        protected FilterDefinition<TDAO> GetBaseFilter()
        {
            return Builders<TDAO>.Filter.GetBaseFilter("TableName", _tableName);
        }

        protected FilterDefinitionBuilder<TDAO> GetFilterBuilder()
        {
            return Builders<TDAO>.Filter;
        }
    }
}
