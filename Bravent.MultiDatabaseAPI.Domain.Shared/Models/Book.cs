using System;
namespace Bravent.MultiDatabaseAPI.Domain.Shared.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }

        public Author Author { get; set; }
    }
}
