using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.DAO
{
    [Table("Author")]
    public class Author : IDAO<Guid?>
    {
        [Key]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<Book> Books { get; set; }

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
