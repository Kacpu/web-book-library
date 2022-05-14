using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.PublisherDTO;
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
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _publisherRepository;

        public PublisherService(IRepository<Publisher> publisherReposiotry)
        {
            _publisherRepository = publisherReposiotry;
        }

        public async Task<PublisherResponse> GetByIdAsync(int id)
        {
            var category = await _publisherRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException("publisher not found");
            }

            return await Task.FromResult(category.ToResponse());
        }

        public async Task<IEnumerable<PublisherResponse>> BrowseAllAsync(string name)
        {
            Expression<Func<Publisher, bool>> filter = PredicateBuilder.New<Publisher>();

            if (!string.IsNullOrEmpty(name))
            {
                filter = filter.And(p => p.Name.Contains(name));
            }

            var ps = await _publisherRepository.BrowseAllAsync(filter);
            return await Task.FromResult(ps.Select(x => x.ToResponse()));
        }

        public async Task<PublisherResponse> CreateAsync(PublisherCreate publisherCreate)
        {
            var p = await _publisherRepository.CreateAsync(publisherCreate.ToDomain());
            return await Task.FromResult(p.ToResponse());
        }

        public async Task<PublisherResponse> UpdateAsync(int id, PublisherUpdate publisherUpdate)
        {
            var p = await _publisherRepository.GetByIdAsync(id);

            if (p is null)
            {
                throw new NotFoundException("publisher not found");
            }

            p.Name = !string.IsNullOrEmpty(publisherUpdate.Name) ? publisherUpdate.Name : p.Name;

            p = await _publisherRepository.UpdateAsync(p);
            return await Task.FromResult(p.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _publisherRepository.GetByIdAsync(id);

            if (p is null)
            {
                throw new NotFoundException("publisher not found");
            }

            await _publisherRepository.DeleteAsync(id);
        }
    }
}
