using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Order;
using HotpotWebApplication.Models;
using HotpotWebApplication.Models.Enums;
using HotpotWebApplication.Repositories.Implementations;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Services.Implementations
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ICartRepository _cartRepository;
        private readonly ApplicationDbContext _context;
        public OrderService(IOrderRepository repository, ICartRepository cartRepository,ApplicationDbContext context)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _context=   context;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

       
            public async Task<Order> CreateOrderAsync(Order order)
        {
            await _repository.AddAsync(order);
            await _repository.SaveAsync();
            return order;
        
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _repository.UpdateAsync(order);
            await _repository.SaveAsync();
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                return false;

            await _repository.DeleteAsync(order);
            await _repository.SaveAsync();

            return true;
        }
        public async Task<Order> PlaceOrderAsync(PlaceOrderDto dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty");
            }

            decimal totalAmount = cart.CartItems.Sum(
    x => x.Quantity * x.UnitPrice);
            var order = new Order
            {
                UserId = dto.UserId,
                RestaurantId = cart.CartItems.First().MenuItem.RestaurantId,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = dto.ShippingAddress,
                Notes = dto.Notes,
                
                TotalAmount = totalAmount,
                OrderStatus = OrderStatus.Pending
            };
            await _repository.AddAsync(order);
            await _repository.SaveAsync();
            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
            foreach (var item in cart.CartItems)
            {
                await _cartRepository.RemoveCartItemAsync(item);
            }

            await _cartRepository.SaveAsync();
            return order;

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _repository.GetOrdersByUserIdAsync(userId);
        }
        public async Task<IEnumerable<Order>> GetOrdersByRestaurantIdAsync(int restaurantId)
        {
            return await _repository.GetOrdersByRestaurantIdAsync(restaurantId);
        }

    }
}
