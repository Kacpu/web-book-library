using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.AuthorDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var a = await _authorService.GetByIdAsync(id);
            return Json(a);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseAuthors(string name, string surname)
        {
            var a = await _authorService.BrowseAllAsync(name, surname);
            return Json(a);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorCreate authorCreate)
        {
            var a = await _authorService.CreateAsync(authorCreate);
            return CreatedAtAction(nameof(GetAuthorById), new { id = a.Id }, a);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorUpdate authorUpdate, int id)
        {
            var a = await _authorService.UpdateAsync(id, authorUpdate);
            return Json(a);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
