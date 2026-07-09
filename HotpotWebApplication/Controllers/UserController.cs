using HotpotWebApplication.DTOs.User;
using HotpotWebApplication.Models;
using HotpotWebApplication.Services.Implementations;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users =
                await _service.GetAllUsersAsync();

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var user = await _service.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
     int id,
     UpdateUserDto dto)
        {
            var updated =
                await _service.UpdateUserAsync(id, dto);

            if (!updated)
                return NotFound();

            return Ok("Profile Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _service.DeleteUserAsync(id);

            return Ok("User deleted successfully.");
        }
    }
}
