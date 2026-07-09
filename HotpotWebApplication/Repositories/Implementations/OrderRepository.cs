using HotpotWebApplication.Data;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Repositories.Implementations
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders

        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)

        .Include(o => o.Payments)

        .Include(o => o.User)

        .Include(o => o.Restaurant)

        .FirstOrDefaultAsync(o => o.OrderId == id);
        }
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>>
    GetOrdersByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
