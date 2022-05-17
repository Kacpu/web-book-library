using BookLibrary.Infrastructure.DTO.LibraryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface ILibraryService
    {
        Task<LibraryResponse> GetByIdAsync(int id);
        Task<IEnumerable<LibraryResponse>> BrowseAllAsync(string name = null, int? userId = null);
        Task<LibraryResponse> CreateAsync(LibraryCreate libraryCreate);
        Task<LibraryResponse> UpdateAsync(int id, LibraryUpdate libraryUpdate);
        Task DeleteAsync(int id);
        Task AddBookAsync(int libId, int bookId);
        Task RemoveBookAsync(int libId, int bookId);
    }
}
