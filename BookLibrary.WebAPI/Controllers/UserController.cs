using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var u = await _userService.GetByIdAsync(id);
            return Json(u);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseUsers(string username)
        {
            var u = await _userService.BrowseAllAsync(username);
            return Json(u);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserCreate userCreate)
        {
            var u = await _userService.CreateAsync(userCreate);
            return CreatedAtAction(nameof(GetUserById), new { id = u.Id }, u);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate, int id)
        {
            var u = await _userService.UpdateAsync(id, userUpdate);
            return Json(u);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
