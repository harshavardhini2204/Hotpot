using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.MenuItem;
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
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ILogger<MenuItemController> _logger;
        public MenuItemController(IMenuItemService menuItemService, ILogger<MenuItemController> logger)
        {
            _menuItemService= menuItemService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems =
                await _menuItemService.GetAllMenuItemsAsync();

            return Ok(menuItems);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menu =
               await _menuItemService.GetMenuItemByIdAsync(id);

            if (menu == null)
                return NotFound();

            var dto = new MenuItemResponseDto
            {
                MenuItemId = menu.MenuItemId,
                ItemName = menu.ItemName,
                Price = menu.Price
            };

            return Ok(dto);
        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpPost]
        public async Task<IActionResult>CreateMenuItem(CreateMenuDto dto)
        {
            var menuItem = new MenuItem
            {
                RestaurantId = dto.RestaurantId,
                CategoryId = dto.CategoryId,
                ItemName = dto.ItemName,
                Description = dto.Description,
                Ingridients = dto.Ingridients,
                Price = dto.Price,
                DiscountPrice = dto.DiscountPrice,
                CookingTime = dto.CookingTime,
                AvailabilityTime = dto.AvailabilityTime,
                DietaryType = dto.DietaryType,
                TasteInfo = dto.TasteInfo,
                Calories = dto.Calories,
                Protein = dto.Protein,
                Carbs = dto.Carbs,
                Fat = dto.Fat,
                ImageUrl = dto.ImageUrl,
                IsAvailable = dto.IsAvailable,
                CreatedDate = DateTime.Now
            };

            var createdMenuItem =
                await _menuItemService.CreateMenuItemAsync(menuItem);

            _logger.LogInformation(
                "Menu item added: {ItemName}",
                createdMenuItem.ItemName);

            return CreatedAtAction(
                nameof(GetMenuItemById),
                new { id = createdMenuItem.MenuItemId },
                createdMenuItem);
        }
        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateMenuItem(int id,UpdateMenuDto dto)
        {
            var menuItem =
                await _menuItemService.GetMenuItemByIdAsync(id);

            if (menuItem == null)
                return NotFound();

            menuItem.RestaurantId = dto.RestaurantId;
            menuItem.CategoryId = dto.CategoryId;
            menuItem.ItemName = dto.ItemName;
            menuItem.Description = dto.Description;
            menuItem.Ingridients = dto.Ingridients;
            menuItem.Price = dto.Price;
            menuItem.DiscountPrice = dto.DiscountPrice;
            menuItem.CookingTime = dto.CookingTime;
            menuItem.AvailabilityTime = dto.AvailabilityTime;
            menuItem.DietaryType = dto.DietaryType;
            menuItem.TasteInfo = dto.TasteInfo;
            menuItem.Calories = dto.Calories;
            menuItem.Protein = dto.Protein;
            menuItem.Carbs = dto.Carbs;
            menuItem.Fat = dto.Fat;
            menuItem.ImageUrl = dto.ImageUrl;
            menuItem.IsAvailable = dto.IsAvailable;

            await _menuItemService.UpdateMenuItemAsync(menuItem);

            return Ok(menuItem);
        }
        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var deleted =
                 await _menuItemService.DeleteMenuItemAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Menu item deleted successfully");
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchMenuItems(string keyword)
        {
            var result =
                await _menuItemService.SearchMenuItemsAsync(keyword);

            return Ok(result);
        }


    }
}
