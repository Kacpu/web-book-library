using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.AuthorDTO;
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
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorReposiotry)
        {
            _authorRepository = authorReposiotry;
        }

        public async Task<AuthorResponse> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if(author is null)
            {
                throw new NotFoundException("author not found");
            }

            return await Task.FromResult(author.ToResponse());
        }

        public async Task<IEnumerable<AuthorResponse>> BrowseAllAsync(string name, string surname)
        {
            Expression<Func<Author, bool>> filter = PredicateBuilder.New<Author>();

            if (!string.IsNullOrEmpty(name))
            {
                filter = filter.And(author => author.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(surname))
            {
                filter = filter.And(author => author.Surname.Contains(surname));
            }

            var authors = await _authorRepository.BrowseAllAsync(filter);
            return await Task.FromResult(authors.Select(a => a.ToResponse()));
        }

        public async Task<AuthorResponse> CreateAsync(AuthorCreate authorCreate)
        {
            var a = await _authorRepository.CreateAsync(authorCreate.ToDomain());
            return await Task.FromResult(a.ToResponse());
        }

        public async Task<AuthorResponse> UpdateAsync(int id, AuthorUpdate authorUpdate)
        {
            var a = await _authorRepository.GetByIdAsync(id);

            if (a is null)
            {
                throw new NotFoundException("author not found");
            }

            a.Name = !string.IsNullOrEmpty(authorUpdate.Name) ? authorUpdate.Name : a.Name;
            a.Surname = !string.IsNullOrEmpty(authorUpdate.Surname) ? authorUpdate.Surname : a.Surname;
            a.Description = !string.IsNullOrEmpty(authorUpdate.Description) ? authorUpdate.Description : a.Description;
            a.DateOfBirth = authorUpdate.DateOfBirth ?? a.DateOfBirth;

            a = await _authorRepository.UpdateAsync(a);
            return await Task.FromResult(a.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var a = await _authorRepository.GetByIdAsync(id);

            if (a is null)
            {
                throw new NotFoundException("author not found");
            }

            await _authorRepository.DeleteAsync(id);
        }
    }
}
