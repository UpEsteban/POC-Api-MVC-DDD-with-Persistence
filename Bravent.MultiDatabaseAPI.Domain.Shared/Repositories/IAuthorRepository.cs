using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Domain.Shared.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> Insert(Author item);
        Task<Author> Get(string id);
    }
}
