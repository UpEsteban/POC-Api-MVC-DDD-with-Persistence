using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.DAO
{
    [Table("Book")]
    public class Book : IDAO<Guid?>
    {
        [Key]
        public Guid? Id { get; set; }
        [ForeignKey("Author")]
        public Guid? AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }

        public Author Author { get; set; }

        public Guid? GetId()
        {
            return Id;
        }

        public void SetNewId()
        {
            Id = Guid.NewGuid();
        }
    }
}
