using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class Library : Entity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
