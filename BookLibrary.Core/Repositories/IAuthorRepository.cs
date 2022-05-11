using BookLibrary.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetAsync(int id);
        Task<ICollection<Author>> BrowseAllAsync();
        Task<Author> AddAsync(Author author);
        Task UpdateAsync();
        Task DelAsync(int id);
    }
}
