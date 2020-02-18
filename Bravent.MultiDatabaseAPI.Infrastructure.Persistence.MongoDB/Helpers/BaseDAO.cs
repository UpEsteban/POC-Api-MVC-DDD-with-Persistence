using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers
{
    public class BaseDAO
    {
        public BaseDAO(string tableName)
        {
            TableName = tableName;
        }

        [BsonElement("TableName")]
        public string TableName { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
