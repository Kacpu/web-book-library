using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.BookSeriesDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookSeriesController : Controller
    {
        private readonly IBookSeriesService _bookSeriesService;

        public BookSeriesController(IBookSeriesService bookSeriesService)
        {
            _bookSeriesService = bookSeriesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookSeriesById(int id)
        {
            var bs = await _bookSeriesService.GetByIdAsync(id);
            return Json(bs);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseBooksSeries(string name)
        {
            var bs = await _bookSeriesService.BrowseAllAsync(name);
            return Json(bs);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookSeries([FromBody] BookSeriesCreate bookSeriesCreate)
        {
            var bs = await _bookSeriesService.CreateAsync(bookSeriesCreate);
            return CreatedAtAction(nameof(GetBookSeriesById), new { id = bs.Id }, bs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookSeries([FromBody] BookSeriesUpdate bookSeriesUpdate, int id)
        {
            var bs = await _bookSeriesService.UpdateAsync(id, bookSeriesUpdate);
            return Json(bs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookSeries(int id)
        {
            await _bookSeriesService.DeleteAsync(id);
            return NoContent();
        }
    }
}
