using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.LibraryDTO
{
    public class LibraryCreate
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }

        public int OwnerId { get; set; }
    }
}
