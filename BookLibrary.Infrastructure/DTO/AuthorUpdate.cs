using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO
{
    public class AuthorUpdate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
