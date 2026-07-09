using BCrypt.Net;
using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Auth;
using HotpotWebApplication.Models;
using HotpotWebApplication.Services;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        
        public AuthController(ApplicationDbContext context, IJwtService jwtService,ILogger<AuthController>logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existinguser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existinguser != null)
            {
                return BadRequest(new
                {
                    Message = "Email already exists"
                });
            }
            string role;

            var email = registerDto.Email.ToLower();

            if (email.EndsWith("@admin.com"))
            {
                role = "Admin";
            }
            else if (email.EndsWith("@restaurant.com"))
            {
                role = "RestaurantOwner";
            }
            else
            {
                role = "Customer";
            }

            var user = new User
            {
                FullName = registerDto.FullName,
                Gender = registerDto.Gender,
                Email = registerDto.Email,
                Address = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = role,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("New user registered: {Email}",user.Email);
            return Ok(new
            {
                Message = "User registered successfully",
                UserId = user.UserId
            });


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.Email== loginDto.Email);
            if (user == null) return Unauthorized(new { message = "Invalid email or password" });
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if(!isPasswordValid) return Unauthorized(new { message = "Invalid email or password" });
            var token = _jwtService.GenerateToken(user.UserId,user.Email,user.Role);
            _logger.LogInformation("User logged in: {Email}", user.Email);
            int? restaurantId = null;

            if (user.Role == "RestaurantOwner")
            {
                restaurantId = await _context.Restaurants
                    .Where(r => r.UserId == user.UserId)
                    .Select(r => (int?)r.RestaurantId)
                    .FirstOrDefaultAsync();
            }

            return Ok(new
            {
                Token = token,
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role,
                RestaurantId = restaurantId
            });

        }
    }
}
