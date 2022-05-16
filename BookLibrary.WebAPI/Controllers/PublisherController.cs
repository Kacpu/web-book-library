using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.PublisherDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var p = await _publisherService.GetByIdAsync(id);
            return Json(p);
        }

        [HttpGet]
        public async Task<IActionResult> BrowsePublishers(string name)
        {
            var p = await _publisherService.BrowseAllAsync(name);
            return Json(p);
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherCreate publisherCreate)
        {
            var p = await _publisherService.CreateAsync(publisherCreate);
            return CreatedAtAction(nameof(GetPublisherById), new { id = p.Id }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] PublisherUpdate publisherUpdate, int id)
        {
            var p = await _publisherService.UpdateAsync(id, publisherUpdate);
            return Json(p);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await _publisherService.DeleteAsync(id);
            return NoContent();
        }
    }
}
