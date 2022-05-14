using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface IBookService
    {
        Task<AuthorResponse> GetByIdAsync(int id);
        Task<IEnumerable<AuthorResponse>> BrowseAllAsync(string name = null, string surname = null);
        Task<AuthorResponse> CreateAsync(AuthorCreate authorCreate);
        Task<AuthorResponse> UpdateAsync(int id, AuthorUpdate authorUpdate);
        Task DeleteAsync(int id);
    }
}
