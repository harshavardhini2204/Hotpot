using HotpotWebApplication.DTOs.Order;
using HotpotWebApplication.Models;

namespace HotpotWebApplication.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
