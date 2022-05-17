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
        private readonly IRepository<Book> _bookRepository;

        public LibraryService(IRepository<Library> libraryReposiotry, IRepository<Book> bookReposiotry)
        {
            _libraryRepository = libraryReposiotry;
            _bookRepository = bookReposiotry;
        }

        public async Task<LibraryResponse> GetByIdAsync(int id)
        {
            string[] includeProperties = { "Owner", "Books" };

            var library = await _libraryRepository.GetByIdAsync(id, includeProperties);

            if (library is null)
            {
                throw new NotFoundException("library not found");
            }

            return await Task.FromResult(library.ToResponse());
        }

        public async Task<IEnumerable<LibraryResponse>> BrowseAllAsync(string name, int? userId)
        {
            Expression<Func<Library, bool>> filter = PredicateBuilder.New<Library>(true);

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
            if (!await _libraryRepository.IsExist<User>(libraryCreate.OwnerId))
            {
                throw new NotFoundException("owner not found");
            }

            var checkL = await _libraryRepository
                .BrowseAllAsync(x => x.Name == libraryCreate.Name);

            if (checkL.Any())
            {
                throw new BadRequestException("library already exists");
            }

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

            if (!string.IsNullOrEmpty(libraryUpdate.Name))
            {
                var checkL = await _libraryRepository
                .BrowseAllAsync(x => x.Name == libraryUpdate.Name);

                if (checkL.Any())
                {
                    throw new BadRequestException("library already exists");
                }

                l.Name = libraryUpdate.Name;
            }

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

        public async Task AddBookAsync(int libId, int bookId)
        {
            string[] includeProperties = { "Books" };

            var library = await _libraryRepository.GetByIdAsync(libId, includeProperties);

            if (library is null)
            {
                throw new NotFoundException("library not found");
            }

            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book is null)
            {
                throw new NotFoundException("book not found");
            }

            if(library.Books is null)
            {
                library.Books = new List<Book>() { book };
            }
            else if (library.Books.Contains(book))
            {
                throw new BadRequestException("book is already in the library");
            }
            else
            {
                library.Books.Add(book);
            }

            await _libraryRepository.SaveAsync();
        }

        public async Task RemoveBookAsync(int libId, int bookId)
        {
            string[] includeProperties = { "Books" };

            var library = await _libraryRepository.GetByIdAsync(libId, includeProperties);

            if (library is null)
            {
                throw new NotFoundException("library not found");
            }

            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book is null)
            {
                throw new NotFoundException("book not found");
            }

            if (library.Books?.Contains(book) ?? false)
            {
                library.Books.Remove(book);
            }
            else
            {
                throw new BadRequestException("book is not in the library");
            }

            await _libraryRepository.SaveAsync();
        }
    }
}
