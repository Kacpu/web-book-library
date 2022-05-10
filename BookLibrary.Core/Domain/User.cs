using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
