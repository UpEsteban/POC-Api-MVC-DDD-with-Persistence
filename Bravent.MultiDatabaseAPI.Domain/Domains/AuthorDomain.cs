using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Domain.Domains
{
    public class AuthorDomain : IAuthorDomain
    {
        private readonly IAuthorRepository _repository;

        public AuthorDomain(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Author> Insert(Author item)
        {
            return await _repository.Insert(item);
        }
    }
}
