using BookLibrary.Infrastructure.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.AbstractServices
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<IEnumerable<CategoryResponse>> BrowseAllAsync(string name = null);
        Task<CategoryResponse> CreateAsync(CategoryCreate categoryCreate);
        Task<CategoryResponse> UpdateAsync(int id, CategoryUpdate categoryUpdate);
        Task DeleteAsync(int id);
    }
}
