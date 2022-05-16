using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.LibraryDTO;
using BookLibrary.Infrastructure.Exceptions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IRepository<Library> _libraryRepository;

        public LibraryService(IRepository<Library> libraryReposiotry)
        {
            _libraryRepository = libraryReposiotry;
        }

        public async Task<LibraryResponse> GetByIdAsync(int id)
        {
            string[] includeProperties = { "User", "Books" };

            var library = await _libraryRepository.GetByIdAsync(id, includeProperties);

            if (library is null)
            {
                throw new NotFoundException("library not found");
            }

            return await Task.FromResult(library.ToResponse());
        }

        public async Task<IEnumerable<LibraryResponse>> BrowseAllAsync(string name, int? userId)
        {
            Expression<Func<Library, bool>> filter = PredicateBuilder.New<Library>();

            if (!string.IsNullOrEmpty(name))
            {
                filter = filter.And(l => l.Name.Contains(name));
            }

            if (userId != null)
            {
                filter = filter.And(l => l.OwnerId == userId);
            }

            string[] includeProperties = { "Owner", "Books" };

            var ls = await _libraryRepository.BrowseAllAsync(filter, includeProperties);
            return await Task.FromResult(ls.Select(x => x.ToResponse()));
        }

        public async Task<LibraryResponse> CreateAsync(LibraryCreate libraryCreate)
        {
            var l = await _libraryRepository.CreateAsync(libraryCreate.ToDomain());
            return await Task.FromResult(l.ToResponse());
        }

        public async Task<LibraryResponse> UpdateAsync(int id, LibraryUpdate libraryUpdate)
        {
            var l = await _libraryRepository.GetByIdAsync(id);

            if (l is null)
            {
                throw new NotFoundException("library not found");
            }

            l.Name = !string.IsNullOrEmpty(libraryUpdate.Name) ? libraryUpdate.Name : l.Name;
            l.Description = !string.IsNullOrEmpty(libraryUpdate.Description) ? libraryUpdate.Description : l.Description;
            l.IsPrivate = libraryUpdate.IsPrivate ?? l.IsPrivate;

            l = await _libraryRepository.UpdateAsync(l);
            return await Task.FromResult(l.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var l = await _libraryRepository.GetByIdAsync(id);

            if (l is null)
            {
                throw new NotFoundException("library not found");
            }

            await _libraryRepository.DeleteAsync(id);
        }
    }
}
