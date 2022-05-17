using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.BookDTO
{
    public class BookCreate
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        [Range(0, 2022)]
        public int? ReleaseYear { get; set; }

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Language { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int PublisherId { get; set; }

        public int? BookSeriesId { get; set; }

        public ICollection<int> CategoriesId { get; set; }
    }
}
