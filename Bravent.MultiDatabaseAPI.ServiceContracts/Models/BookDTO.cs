using System;
namespace Bravent.MultiDatabaseAPI.ServiceContracts.Models
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long PublicationDate { get; set; }
        public string ISBN { get; set; }

        public AuthorDTO Author { get; set; }
    }
}
