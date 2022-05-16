using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.UserDTO
{
    public class UserUpdate
    {
        [StringLength(maximumLength: 100)]
        public string Username { get; set; }
    }
}
