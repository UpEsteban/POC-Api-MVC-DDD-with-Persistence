using System;
using System.Collections.Generic;

namespace Bravent.MultiDatabaseAPI.Domain.Shared.Models
{
    public class Author
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
