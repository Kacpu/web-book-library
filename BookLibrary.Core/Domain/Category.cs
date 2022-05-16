using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class Category : Entity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
