using HotpotWebApplication.Models;

namespace HotpotWebApplication.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetOrdersByRestaurantIdAsync(int restaurantId);
        Task SaveAsync();
    }
}
