using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);

        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if(category == null)
            {
                return NotFound("Category id not found.");
            }
            return Ok(category);
        }
        [Authorize(Roles = "Admin,RestaurantOwner")]

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            var createdCategory =
                await _categoryService.CreateCategoryAsync(category);

            _logger.LogInformation(
                "Category created. CategoryId: {CategoryId}",
                createdCategory.CategoryId);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = createdCategory.CategoryId },
                createdCategory);
        }
        [Authorize(Roles = "Admin,RestaurantOwner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryDto dto)
        {
            var category =
                await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound("Category not found");

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            await _categoryService.UpdateCategoryAsync(category);

            return Ok(category);
        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted =
                await _categoryService.DeleteCategoryAsync(id);

            if (!deleted)
                return NotFound("Category not found");

            return Ok("Category deleted successfully");
        }
    }
}
