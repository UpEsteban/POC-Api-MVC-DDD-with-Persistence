using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Domain.Domains
{
    public interface IAuthorDomain
    {
        Task<Author> Insert(Author item);
    }
}
