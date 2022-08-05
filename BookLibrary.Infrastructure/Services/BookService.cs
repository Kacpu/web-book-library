using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.BookDTO;
using BookLibrary.Infrastructure.Exceptions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Category> categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BookResponse> GetByIdAsync(int id)
        {
            string[] includeProperties = {"Author", "Publisher", "BookSeries", "Categories"};

            var book = await _bookRepository.GetByIdAsync(id, includeProperties);

            if (book is null)
            {
                throw new NotFoundException("book not found");
            }

            return await Task.FromResult(book.ToResponse());
        }

        public async Task<IEnumerable<BookResponse>> BrowseAllAsync(string title, int? authorId, int? publisherId,
            int? bookSeriesId, int? categoryId, int? libraryId, int? skip, int? take, bool isShort)
        {
            Expression<Func<Book, bool>> filter = PredicateBuilder.New<Book>(true);

            if (!string.IsNullOrEmpty(title))
            {
                filter = title.Length >= 3
                    ? filter.And(book => book.Title.Contains(title))
                    : filter.And(book => book.Title.StartsWith(title));
            }

            if (authorId != null)
            {
                filter = filter.And(book => book.AuthorId == authorId);
            }

            if (publisherId != null)
            {
                filter = filter.And(book => book.PublisherId == publisherId);
            }

            if (bookSeriesId != null)
            {
                filter = filter.And(book => book.BookSeriesId == bookSeriesId);
            }

            if (categoryId != null)
            {
                filter = filter.And(book => book.Categories.Any(c => c.Id == categoryId));
            }

            if (libraryId != null)
            {
                filter = filter.And(book => book.Libraries.Any(l => l.Id == libraryId));
            }

            string[] includeProperties = null;
            
            if (!isShort)
            {
                includeProperties = new [] {"Author", "Publisher", "BookSeries", "Categories"};
            }

            var books = await _bookRepository
                .BrowseAllAsync(filter, includeProperties, skip: skip, take: take);

            return await Task.FromResult(books.Select(x => x.ToResponse()));
        }

        public async Task<BookResponse> CreateAsync(BookCreate bookCreate)
        {
            if (!await _bookRepository.IsExist<Author>(bookCreate.AuthorId))
            {
                throw new NotFoundException("author not found");
            }

            if (!await _bookRepository.IsExist<Publisher>(bookCreate.PublisherId))
            {
                throw new NotFoundException("publisher not found");
            }

            if (bookCreate.BookSeriesId != null &&
                !await _bookRepository.IsExist<BookSeries>(bookCreate.BookSeriesId.Value))
            {
                throw new NotFoundException("book series not found");
            }

            var books = await _bookRepository
                .BrowseAllAsync(x => x.Title == bookCreate.Title && x.AuthorId == bookCreate.AuthorId);

            if (books.Any())
            {
                throw new BadRequestException("book already exists");
            }

            var b = bookCreate.ToDomain();

            if (bookCreate.CategoriesId != null)
            {
                var cs = await _categoryRepository.BrowseAllAsync(c => bookCreate.CategoriesId.Any(id => id == c.Id));
                b.Categories = cs.ToList();
            }

            b = await _bookRepository.CreateAsync(b);
            return await Task.FromResult(b.ToResponse());
        }

        public async Task<BookResponse> UpdateAsync(int id, BookUpdate bookUpdate)
        {
            string[] includeProperties = {"Categories"};
            var b = await _bookRepository.GetByIdAsync(id, includeProperties);

            if (b is null)
            {
                throw new NotFoundException("book not found");
            }

            if (bookUpdate.AuthorId != null && !await _bookRepository.IsExist<Author>(bookUpdate.AuthorId.Value))
            {
                throw new NotFoundException("author not found");
            }

            if (bookUpdate.PublisherId != null &&
                !await _bookRepository.IsExist<Publisher>(bookUpdate.PublisherId.Value))
            {
                throw new NotFoundException("publisher not found");
            }

            if (bookUpdate.BookSeriesId != null &&
                !await _bookRepository.IsExist<Author>(bookUpdate.BookSeriesId.Value))
            {
                throw new NotFoundException("book series not found");
            }

            b.Title = !string.IsNullOrEmpty(bookUpdate.Title) ? bookUpdate.Title : b.Title;
            b.ReleaseYear = bookUpdate.ReleaseYear ?? b.ReleaseYear;
            b.Description = !string.IsNullOrEmpty(bookUpdate.Description) ? bookUpdate.Description : b.Description;
            b.Language = !string.IsNullOrEmpty(bookUpdate.Language) ? bookUpdate.Language : b.Language;
            b.NumberOfPages = bookUpdate.NumberOfPages ?? b.NumberOfPages;
            b.AuthorId = bookUpdate.AuthorId ?? b.AuthorId;
            b.PublisherId = bookUpdate.PublisherId ?? b.PublisherId;
            b.BookSeriesId = bookUpdate.BookSeriesId ?? b.BookSeriesId;

            if (!string.IsNullOrEmpty(bookUpdate.Title) || bookUpdate.AuthorId != null)
            {
                var books = await _bookRepository
                    .BrowseAllAsync(x => x.Title == b.Title && x.AuthorId == b.AuthorId);

                if (books.Any())
                {
                    throw new BadRequestException("book already exists");
                }
            }

            if (bookUpdate.CategoriesId != null)
            {
                var cs = await _categoryRepository
                    .BrowseAllAsync(c => bookUpdate.CategoriesId.Any(bcId => bcId == c.Id));
                b.Categories = cs.ToList();
            }

            b = await _bookRepository.UpdateAsync(b);
            return await Task.FromResult(b.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var b = await _bookRepository.GetByIdAsync(id);

            if (b is null)
            {
                throw new NotFoundException("book not found");
            }

            await _bookRepository.DeleteAsync(id);
        }
    }
}