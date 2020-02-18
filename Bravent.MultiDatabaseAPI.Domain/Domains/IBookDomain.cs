using System.Collections.Generic;
using System.Threading.Tasks;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;

namespace Bravent.MultiDatabaseAPI.Domain.Domains
{
    public interface IBookDomain
    {
        Task<IEnumerable<Book>> FindByName(string name);
        Task<Book> FindById(string id);
        Task<Book> FindByISBN(string isbn);
        Task<Book> InsertBook(Book item);
    }
}