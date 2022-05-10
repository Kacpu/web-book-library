using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
