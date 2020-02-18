using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;

namespace Bravent.MultiDatabaseAPI.Domain.Shared.Repositories
{
    public interface IBookRepository
    {
        Task<Book> FindById(string id);
        Task<Book> FindByISBN(string isbn);
        Task<IEnumerable<Book>> FindByName(string name);
        Task<Book> Insert(Book item);
        Task<Book> Update(Book item);
        Task Delete(string id);
    }
}
