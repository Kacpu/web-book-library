using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var c = await _categoryService.GetByIdAsync(id);
            return Json(c);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseCategories(string name)
        {
            var c = await _categoryService.BrowseAllAsync(name);
            return Json(c);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreate categoryCreate)
        {
            var c = await _categoryService.CreateAsync(categoryCreate);
            return CreatedAtAction(nameof(GetCategoryById), new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdate categoryUpdate, int id)
        {
            var c = await _categoryService.UpdateAsync(id, categoryUpdate);
            return Json(c);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
