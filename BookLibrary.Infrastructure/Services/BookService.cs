using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.BookDTO;
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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookReposiotry)
        {
            _bookRepository = bookReposiotry;
        }

        public async Task<BookResponse> GetByIdAsync(int id)
        {
            string[] includeProperties = { "Author", "Publisher", "BookSeries", "Categories" };

            var book = await _bookRepository.GetByIdAsync(id, includeProperties);

            if (book is null)
            {
                throw new NotFoundException("book not found");
            }

            return await Task.FromResult(book.ToResponse());
        }

        public async Task<IEnumerable<BookResponse>> BrowseAllAsync(string title, int? authorId, int? publisherId, int? bookSeriesId, int? categoryId)
        {
            Expression<Func<Book, bool>> filter = PredicateBuilder.New<Book>();

            if (!string.IsNullOrEmpty(title))
            {
                filter = filter.And(book => book.Title.Contains(title));
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

            string[] includeProperties = { "Author", "Publisher", "BookSeries", "Categories" };

            var books = await _bookRepository.BrowseAllAsync(filter, includeProperties);
            return await Task.FromResult(books.Select(x => x.ToResponse()));
        }

        public async Task<BookResponse> CreateAsync(BookCreate bookCreate)
        {
            var b = await _bookRepository.CreateAsync(bookCreate.ToDomain());
            return await Task.FromResult(b.ToResponse());
        }

        public async Task<BookResponse> UpdateAsync(int id, BookUpdate bookUpdate)
        {
            var b = await _bookRepository.GetByIdAsync(id);

            if (b is null)
            {
                throw new NotFoundException("book not found");
            }

            b.Title = !string.IsNullOrEmpty(bookUpdate.Title) ? bookUpdate.Title : b.Title;
            b.ReleaseDate = bookUpdate.ReleaseDate ?? b.ReleaseDate;
            b.Description = !string.IsNullOrEmpty(bookUpdate.Description) ? bookUpdate.Description : b.Description;
            b.Language = !string.IsNullOrEmpty(bookUpdate.Language) ? bookUpdate.Language : b.Language;
            b.NumberOfPages = bookUpdate.NumberOfPages ?? b.NumberOfPages;
            b.AuthorId = bookUpdate.AuthorId ?? b.AuthorId;
            b.PublisherId = bookUpdate.PublisherId ?? b.PublisherId;
            b.BookSeriesId = bookUpdate.BookSeriesId ?? b.BookSeriesId;
            b.Categories = bookUpdate.CategoriesId is not null && bookUpdate.CategoriesId.Count != 0 ?
                bookUpdate.CategoriesId.Select(cId => new Category() { Id = cId }).ToList() : b.Categories;

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
