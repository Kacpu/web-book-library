using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class User : Entity
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Username { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
