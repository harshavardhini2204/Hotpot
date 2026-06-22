using HotpotWebApplication.DTOs.Order;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Implementations;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
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
    }
}
