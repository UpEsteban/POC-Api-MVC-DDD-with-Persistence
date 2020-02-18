using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Domain.Domains
{
    public class BookDomain : IBookDomain
    {
        private readonly IBookRepository _repository;

        public BookDomain(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> FindByName(string name)
        {
            return await _repository.FindByName(name);
        }

        public async Task<Book> InsertBook(Book item)
        {
            return await _repository.Insert(item);
        }

        public async Task<Book> FindByISBN(string isbn)
        {
            return await _repository.FindByISBN(isbn);
        }

        public async Task<Book> FindById(string id)
        {
            return await _repository.FindById(id);
        }
    }
}
