using AutoMapper;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Repositories
{
    public class AuthorRepository : BaseMongoRepository<DAO.Author, string>, IAuthorRepository
    {
        private readonly Persistence<DAO.Author, string> _persistence;
        private readonly Query<DAO.Author, string> _query;
        private readonly IMapper _mapper;

        public AuthorRepository(IPersistence<DAO.Author, string> persistence, IQuery<DAO.Author, string> query, IMapper mapper) : base(DbCollectionCatalog.Author)
        {
            _persistence = (Persistence<DAO.Author, string>)persistence;
            _query = (Query<DAO.Author, string>)query;
            _mapper = mapper;
        }

        private void InsertDAOValidations(DAO.Author item)
        {

        }

        private void UpdateDAOValidations(DAO.Author item)
        {

        }

        private void DeleteDAOValidations()
        {

        }

        public async Task<Author> Insert(Author item)
        {
            DAO.Author dao = _mapper.Map<Author, DAO.Author>(item);
            dao.SetNewId();

            await _persistence.BeginTransactionAsync();
            await _persistence.InsertAsync(dao);
            await _persistence.CommitAsync();

            return _mapper.Map<DAO.Author, Author>(dao);
        }

        public async Task<Author> Get(string id)
        {
            return _mapper.Map<DAO.Author, Author>(await _query.GetAsync(GetBaseFilter().Append(GetFilterBuilder().Eq("_id", new ObjectId(id)))));
        }
    }
}