using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Restaurant;
using HotpotWebApplication.Models;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private ApplicationDbContext _context;
        private readonly ILogger<RestaurantController> _logger;
        public RestaurantController(IRestaurantService service,ApplicationDbContext context,ILogger<RestaurantController>logger)
        {
            _restaurantService = service;
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult>GetRestaurants()
        {
            var restaurants =
                 await _restaurantService.GetAllRestaurantsAsync();

            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetRestaurantById(int id)
        {
            var restaurant =
                await _restaurantService.GetRestaurantByIdAsync(id);

            if (restaurant == null)
                return NotFound("Restaurant not found");

            return Ok(restaurant);
        }
        [HttpGet("owner/{userId}")]
        public async Task<IActionResult> GetRestaurantByOwner(int userId)
        {
            var restaurant = await _restaurantService
                .GetRestaurantByOwnerAsync(userId);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpPost]
        public async Task<IActionResult>CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = new Restaurant
            {
                UserId = dto.UserId,
                RestaurantName = dto.RestaurantName,
                Location = dto.Location,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                Description = dto.Description,
                LogUrl = dto.LogUrl,
                IsActive = dto.IsActive,
                CreatedDate = DateTime.Now
            };

            var createdRestaurant =
                await _restaurantService.CreateRestaurantAsync(restaurant);

            _logger.LogInformation(
                "Restaurant created. RestaurantId: {RestaurantId}",
                createdRestaurant.RestaurantId);

            return CreatedAtAction(
                nameof(GetRestaurantById),
                new { id = createdRestaurant.RestaurantId },
                createdRestaurant);
        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateRestaurant(int id,UpdateRestaurantDto dto)
        {
            var restaurant =
                 await _restaurantService.GetRestaurantByIdAsync(id);

            if (restaurant == null)
                return NotFound("Restaurant not found");

            restaurant.RestaurantName = dto.RestaurantName;
            restaurant.Location = dto.Location;
            restaurant.ContactNumber = dto.ContactNumber;
            restaurant.Email = dto.Email;
            restaurant.Description = dto.Description;
            restaurant.LogUrl = dto.LogUrl;
            restaurant.IsActive = dto.IsActive;

            await _restaurantService.UpdateRestaurantAsync(restaurant);

            _logger.LogInformation(
                "Restaurant updated. RestaurantId: {RestaurantId}",
                restaurant.RestaurantId);

            return Ok(restaurant);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchRestaurants(string keyword)
        {
            var result =
                await _restaurantService.SearchRestaurantsAsync(keyword);

            return Ok(result);
        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpDelete]
        public async Task<IActionResult>DeleteRestaurant(int id)
        {
            var deleted =
                await _restaurantService.DeleteRestaurantAsync(id);

            if (!deleted)
                return NotFound("Restaurant not found");

            _logger.LogInformation(
                "Restaurant deleted. RestaurantId: {RestaurantId}",
                id);

            return Ok("Restaurant deleted successfully");
        }
    }
}
