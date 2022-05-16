using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.LibraryDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibraryById(int id)
        {
            var l = await _libraryService.GetByIdAsync(id);
            return Json(l);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseLibraries(string name, int? userId)
        {
            var l = await _libraryService.BrowseAllAsync(name, userId);
            return Json(l);
        }

        [HttpPost]
        public async Task<IActionResult> AddLibrary([FromBody] LibraryCreate libraryCreate)
        {
            var l = await _libraryService.CreateAsync(libraryCreate);
            return CreatedAtAction(nameof(GetLibraryById), new { id = l.Id }, l);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary([FromBody] LibraryUpdate libraryUpdate, int id)
        {
            var l = await _libraryService.UpdateAsync(id, libraryUpdate);
            return Json(l);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            await _libraryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
