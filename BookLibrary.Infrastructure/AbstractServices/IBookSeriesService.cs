using BookLibrary.Infrastructure.DTO.BookSeriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface IBookSeriesService
    {
        Task<BookSeriesResponse> GetByIdAsync(int id);
        Task<IEnumerable<BookSeriesResponse>> BrowseAllAsync(string name = null);
        Task<BookSeriesResponse> CreateAsync(BookSeriesCreate bookSeeriesCreate);
        Task<BookSeriesResponse> UpdateAsync(int id, BookSeriesUpdate bookSeriesUpdate);
        Task DeleteAsync(int id);
    }
}
