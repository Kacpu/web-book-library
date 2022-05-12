using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = "not found") : base(message) { }
    }
}
