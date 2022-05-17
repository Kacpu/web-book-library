using BookLibrary.Infrastructure.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO.BookDTO
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int NumberOfPages { get; set; }

        public int AuthorId { get; set; }
        public string Author { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public int? BookSeriesId { get; set; }
        public string BookSeriesName { get; set; }

        public ICollection<CategoryResponse> Categories { get; set; }
    }
}
