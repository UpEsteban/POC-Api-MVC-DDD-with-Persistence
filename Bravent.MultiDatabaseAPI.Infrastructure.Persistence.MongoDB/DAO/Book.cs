using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Exceptions;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.DAO
{
    public class Book : BaseDAO, IMongoDAO<string>
    {
        public Book() : base(DbCollectionCatalog.Book)
        {
            
        }

        [BsonElement("AuthorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("PublicationDate")]
        public long PublicationDate { get; set; }
        [BsonElement("ISBN")]
        public string ISBN { get; set; }
        [BsonIgnore]
        public Author Author { get; set; }

        public string GetId()
        {
            return Id;
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

        public string GetTableName()
        {
            return TableName;
        }

    }
}
