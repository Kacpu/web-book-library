using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.AuthorDTO
{
    public class AuthorCreate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
