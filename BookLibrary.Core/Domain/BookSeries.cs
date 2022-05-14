﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class BookSeries : Entity
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
