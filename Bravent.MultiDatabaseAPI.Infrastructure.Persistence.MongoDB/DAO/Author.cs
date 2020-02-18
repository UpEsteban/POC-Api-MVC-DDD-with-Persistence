using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Exceptions;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.DAO
{
    public class Author : BaseDAO, IMongoDAO<string>
    {
        public Author() : base(DbCollectionCatalog.Author)
        {
        }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonIgnore]
        public IEnumerable<Book> Books { get; set; }

        public string GetId()
        {
            return Id;
        }

        public string GetTableName()
        {
            return TableName;
        }

        public void SetNewId()
        {
            if (Id == null)
            {
                Id = ObjectId.GenerateNewId().ToString();
            }
            else
            {
                throw new PersistenceException("Cannot set a new id: Id is not null");
            }
        }


    }
}
