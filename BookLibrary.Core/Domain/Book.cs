using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Domain
{
    public class Book : Entity
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

        public int NumberOfPages { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int? BookSeriesId { get; set; }
        public BookSeries BookSeries { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
