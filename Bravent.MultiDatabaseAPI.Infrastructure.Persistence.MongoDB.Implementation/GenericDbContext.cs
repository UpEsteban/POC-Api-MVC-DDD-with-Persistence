using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation
{
    public class GenericDbContext<TDAO> : IDbContext<TDAO> where TDAO : class
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _mongoClient;
        private readonly MongoDBSettings _settings;
        private IClientSessionHandle _session;

        public GenericDbContext(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            _mongoClient = mongoClient;
            _settings = settings.Value;
            _database = _mongoClient.GetDatabase(_settings.Database);
        }

        internal IMongoCollection<TDAO> GetMongoCollection()
        {
            return _database.GetCollection<TDAO>(_settings.CollectionName);
        }

        public void BeginTransaction()
        {
            _session = _mongoClient.StartSession();
            _session.StartTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            _session = await _mongoClient.StartSessionAsync();
            _session.StartTransaction();
        }

        public void Commit()
        {
            if (_session != null)
            {
                _session.CommitTransaction();
            }
            _session = null;
        }

        public async Task CommitAsync()
        {
            if (_session != null)
            {
                await _session.CommitTransactionAsync();
            }
            _session = null;
        }

        public void Rollback()
        {
            if (_session != null)
            {
                _session.AbortTransaction();
            }
            _session = null;
        }

        public async Task RollbackAsync()
        {
            if (_session != null)
            {
                await _session.AbortTransactionAsync();
            }
            _session = null;
        }
        
    }
}
