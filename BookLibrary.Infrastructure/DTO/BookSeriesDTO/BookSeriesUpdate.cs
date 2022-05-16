﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.BookSeriesDTO
{
    public class  BookSeriesUpdate
    {
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
    }
}
