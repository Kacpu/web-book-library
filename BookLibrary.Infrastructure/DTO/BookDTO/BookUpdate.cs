using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.BookDTO
{
    public class BookUpdate
    {
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        [StringLength(maximumLength: 100)]
        public string Language { get; set; }

        public int? NumberOfPages { get; set; }

        public int? AuthorId { get; set; }

        public int? PublisherId { get; set; }

        public int? BookSeriesId { get; set; }

        public ICollection<int> CategoriesId { get; set; }
    }
}
