using System.Collections.Generic;
using System.Threading.Tasks;
using Bravent.MultiDatabaseAPI.ServiceContracts.Models;

namespace Bravent.MultiDatabaseAPI.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> FindByName(string name);
        Task<BookDTO> FindByISBN(string isbn);
        Task<BookDTO> FindById(string id);
        Task<BookDTO> Insert(BookDTO book);
    }
}