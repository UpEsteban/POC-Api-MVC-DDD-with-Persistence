using AutoMapper;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Implementation;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Persistence<DAO.Author, Guid?> _persistence;
        private readonly Query<DAO.Author, Guid?> _query;
        private readonly IMapper _mapper;

        public AuthorRepository(IPersistence<DAO.Author, Guid?> persistence, IQuery<DAO.Author, Guid?> query, IMapper mapper)
        {
            _persistence = (Persistence<DAO.Author, Guid?>)persistence;
            _query = (Query<DAO.Author, Guid?>)query;
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
            return _mapper.Map<DAO.Author, Author>(await _query.GetAsync(new Guid(id)));
        }
    }
}
