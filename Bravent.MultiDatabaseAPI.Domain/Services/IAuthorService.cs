using Bravent.MultiDatabaseAPI.ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Domain.Services
{
    public interface IAuthorService
    {
        Task<AuthorDTO> Insert(AuthorDTO item);
    }
}
