using System;
using System.Collections.Generic;
using System.Text;

namespace Bravent.MultiDatabaseAPI.ServiceContracts.Models
{
    public class AuthorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<BookDTO> Books { get; set; }
    }
}
