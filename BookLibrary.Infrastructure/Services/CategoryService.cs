using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.CategoryDTO;
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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryReposiotry)
        {
            _categoryRepository = categoryReposiotry;
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException("category not found");
            }

            return await Task.FromResult(category.ToResponse());
        }

        public async Task<IEnumerable<CategoryResponse>> BrowseAllAsync(string name)
        {
            Expression<Func<Category, bool>> filter = PredicateBuilder.New<Category>();

            if (!string.IsNullOrEmpty(name))
            {
                filter = filter.And(c => c.Name.Contains(name));
            }

            var categories = await _categoryRepository.BrowseAllAsync(filter);
            return await Task.FromResult(categories.Select(x => x.ToResponse()));
        }

        public async Task<CategoryResponse> CreateAsync(CategoryCreate categoryCreate)
        {
            var c = await _categoryRepository.CreateAsync(categoryCreate.ToDomain());
            return await Task.FromResult(c.ToResponse());
        }

        public async Task<CategoryResponse> UpdateAsync(int id, CategoryUpdate categoryUpdate)
        {
            var c = await _categoryRepository.GetByIdAsync(id);

            if (c is null)
            {
                throw new NotFoundException("category not found");
            }

            c.Name = !string.IsNullOrEmpty(categoryUpdate.Name) ? categoryUpdate.Name : c.Name;

            c = await _categoryRepository.UpdateAsync(c);
            return await Task.FromResult(c.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _categoryRepository.GetByIdAsync(id);

            if (c is null)
            {
                throw new NotFoundException("category not found");
            }

            await _categoryRepository.DeleteAsync(id);
        }
    }
}
