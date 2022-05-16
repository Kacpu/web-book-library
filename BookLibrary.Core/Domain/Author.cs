using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class Author : Entity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [StringLength(maximumLength: 100)]
        public string Surname { get; set; }

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
