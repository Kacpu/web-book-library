using BookLibrary.Infrastructure.DTO.PublisherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface IPublisherService
    {
        Task<PublisherResponse> GetByIdAsync(int id);
        Task<IEnumerable<PublisherResponse>> BrowseAllAsync(string name = null);
        Task<PublisherResponse> CreateAsync(PublisherCreate publisherCreate);
        Task<PublisherResponse> UpdateAsync(int id, PublisherUpdate publisherUpdate);
        Task DeleteAsync(int id);
    }
}
