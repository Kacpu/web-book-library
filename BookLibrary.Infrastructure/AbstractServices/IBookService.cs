using BookLibrary.Infrastructure.DTO.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface IBookService
    {
        Task<BookResponse> GetByIdAsync(int id);

        Task<IEnumerable<BookResponse>> BrowseAllAsync(string title = null, int? authorId = null,
            int? publisherId = null, int? bookSeriesId = null, int? categoryId = null, int? libraryId = null,
            int? skip = null, int? take = null);

        Task<BookResponse> CreateAsync(BookCreate bookCreate);
        Task<BookResponse> UpdateAsync(int id, BookUpdate bookUpdate);
        Task DeleteAsync(int id);
    }
}