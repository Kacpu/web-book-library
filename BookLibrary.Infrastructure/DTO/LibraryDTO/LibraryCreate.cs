using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.LibraryDTO
{
    public class LibraryCreate
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}
