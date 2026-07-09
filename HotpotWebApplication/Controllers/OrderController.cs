
using HotpotWebApplication.DTOs.Order;
using HotpotWebApplication.Models;
using HotpotWebApplication.Models.Enums;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController>logger)
        {
            _orderService = orderService;
            _logger= logger;
        }
        [Authorize]
        [HttpPost("placeOrder")]
        public async  Task<IActionResult>PlaceOrder(PlaceOrderDto dto)
        {
            var order =
        await _orderService.PlaceOrderAsync(dto);

            return Ok(order);


        }
        [Authorize(Roles = "RestaurantOwner")]
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantOrders(int restaurantId)
        {
            var orders =
                await _orderService.GetOrdersByRestaurantIdAsync(restaurantId);

            return Ok(orders);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult>GetOrders()
        {
            var orders =
              await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order =
                await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound("Order not found");

            return Ok(order);

        }
        [Authorize(Roles ="Admin,RestaurantOwner")]
        [HttpPut("status/{id}")]
        public async Task<IActionResult>UpdateOrderStatus(int id,[FromBody]OrderStatus dto)
        {
            var order =
               await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound("Order not found");

            order.OrderStatus = dto;

            await _orderService.UpdateOrderAsync(order);



            return Ok(new
            {
                Message = "Order status updated successfully",
                OrderId = order.OrderId,
                Status = order.OrderStatus
            });

        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteOrder(int id)
        {
            var deleted =
               await _orderService.DeleteOrderAsync(id);

            if (!deleted)
                return NotFound("Order not found");

            _logger.LogInformation(
                "Order deleted. OrderId: {OrderId}",
                id);

            return Ok("Order deleted successfully");
        }
        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            return Ok(orders);
        }
    }
}
