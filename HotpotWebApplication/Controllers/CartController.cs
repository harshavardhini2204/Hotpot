using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Cart;
using HotpotWebApplication.Models;
using HotpotWebApplication.Responses;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService,ILogger<CartController>logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            await _cartService.AddToCartAsync(dto);

            _logger.LogInformation(
                "Item added to cart. UserId: {UserId}, MenuItemId: {MenuItemId}",
                dto.UserId,
                dto.MenuItemId);

            return Ok(new ApiResponse<MenuItem>
            {
                Success = true,
                Message = "Cart added successfully"
               
            });



            }
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult>GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
                return NotFound("Cart not found");

            return Ok(cart);
        }
        [Authorize]
        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(
    int cartItemId,
    int quantity)
        {
            var updated = await _cartService
        .UpdateQuantityAsync(cartItemId, quantity);

            if (!updated)
                return NotFound("Cart item not found");

            return Ok("Quantity updated successfully");
        }
        [Authorize]
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult>RemoveCartItem(int cartItemId)
        {
            var removed = await _cartService.RemoveCartItemAsync(cartItemId);

            if (!removed)
                return NotFound("Cart item not found");

            _logger.LogInformation(
                "Cart item removed. CartItemId: {CartItemId}",
                cartItemId);

            return Ok("Cart item removed");
        }
    }
}
