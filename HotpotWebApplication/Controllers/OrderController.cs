
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
            var order = new Order
            {
                UserId = dto.UserId,
                RestaurantId = dto.RestaurantId,
                ShippingAddress = dto.ShippingAddress,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Pending
            };

            var createdOrder =
                await _orderService.CreateOrderAsync(order);

            _logger.LogInformation(
                "Order created. OrderId: {OrderId}",
                createdOrder.OrderId);

            return Ok(createdOrder);

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
        public async Task<IActionResult>UpdateOrderStatus(int id,OrderStatus dto)
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
    }
}
