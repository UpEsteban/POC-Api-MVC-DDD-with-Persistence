using System;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers
{
    public interface IMongoDAO<TID> : IDAO<TID>
    {
        string GetTableName();
    }
}
