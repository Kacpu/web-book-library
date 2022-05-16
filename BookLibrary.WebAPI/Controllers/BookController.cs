using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.BookDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var b = await _bookService.GetByIdAsync(id);
            return Json(b);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseBooks(string title, int? authorId, int? publisherId, int? bookSeriesId, int? categoryId)
        {
            var b = await _bookService.BrowseAllAsync(title, authorId, publisherId, bookSeriesId, categoryId);
            return Json(b);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreate bookCreate)
        {
            var b = await _bookService.CreateAsync(bookCreate);
            return CreatedAtAction(nameof(GetBookById), new { id = b.Id }, b);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookUpdate bookUpdate, int id)
        {
            var b = await _bookService.UpdateAsync(id, bookUpdate);
            return Json(b);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
