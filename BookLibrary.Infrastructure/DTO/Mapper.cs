using BookLibrary.Core.Domain;
using BookLibrary.Infrastructure.DTO.AuthorDTO;
using BookLibrary.Infrastructure.DTO.BookDTO;
using BookLibrary.Infrastructure.DTO.BookSeriesDTO;
using BookLibrary.Infrastructure.DTO.CategoryDTO;
using BookLibrary.Infrastructure.DTO.LibraryDTO;
using BookLibrary.Infrastructure.DTO.PublisherDTO;
using BookLibrary.Infrastructure.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO
{
    public static class Mapper
    {
        public static Author ToDomain(this AuthorCreate author)
        {
            return new Author()
            {
                Name = author.Name,
                Surname = author.Surname,
                Description = author.Description,
                DateOfBirth = author.DateOfBirth
            };
        }

        public static AuthorResponse ToResponse(this Author author)
        {
            return new AuthorResponse()
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Description = author.Description,
                DateOfBirth = author.DateOfBirth
            };
        }

        public static Book ToDomain(this BookCreate book)
        {
            return new Book()
            {
                Title = book.Title,
                Description = book.Description,
                ReleaseDate = book.ReleaseDate,
                Language = book.Language,
                NumberOfPages = book.NumberOfPages,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                BookSeriesId = book.BookSeriesId,
            };
        }

        public static BookResponse ToResponse(this Book book)
        {
            return new BookResponse()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ReleaseDate = book.ReleaseDate,
                Language = book.Language,
                NumberOfPages = book.NumberOfPages,
                AuthorId = book.AuthorId,
                Author = book.Author?.Name + " " + book.Author?.Surname,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher?.Name,
                BookSeriesId = book.BookSeriesId,
                BookSeriesName = book.BookSeries?.Name,
                Categories = book.Categories?.Select(c => c.ToResponse()).ToList(),
            };
        }

        public static BookSeries ToDomain(this BookSeriesCreate bookSeries)
        {
            return new BookSeries()
            {
                Name = bookSeries.Name,
            };
        }

        public static BookSeriesResponse ToResponse(this BookSeries bookSeries)
        {
            return new BookSeriesResponse()
            {
                Id = bookSeries.Id,
                Name = bookSeries.Name,
                NumberOfBooks = bookSeries?.Books.Count ?? 0
            };
        }

        public static Category ToDomain(this CategoryCreate category)
        {
            return new Category()
            {
                Name = category.Name,
            };
        }

        public static CategoryResponse ToResponse(this Category category)
        {
            return new CategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public static Library ToDomain(this LibraryCreate library)
        {
            return new Library()
            {
                Name = library.Name,
                Description = library.Description,
                IsPrivate = library.IsPrivate,
                OwnerId = library.OwnerId
            };
        }

        public static LibraryResponse ToResponse(this Library library)
        {
            return new LibraryResponse()
            {
                Id = library.Id,
                Name = library.Name,
                Description = library.Description,
                CreationDate = library.CreationDate,
                IsPrivate = library.IsPrivate,
                OwnerId = library.OwnerId,
                OwnerName = library.Owner?.Username,
                BookNumbers = library.Books?.Count ?? 0,
            };
        }

        public static Publisher ToDomain(this PublisherCreate publisher)
        {
            return new Publisher()
            {
                Name = publisher.Name,
            };
        }

        public static PublisherResponse ToResponse(this Publisher publisher)
        {
            return new PublisherResponse()
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public static User ToDomain(this UserCreate user)
        {
            return new User()
            {
                Username = user.Username,
            };
        }

        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Username = user.Username
            };
        }

    }
}
