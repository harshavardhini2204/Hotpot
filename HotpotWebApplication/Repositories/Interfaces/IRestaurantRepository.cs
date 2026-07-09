using HotpotWebApplication.Models;

namespace HotpotWebApplication.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant?> GetRestaurantByOwnerAsync(int userId);
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string keyword);
        Task SaveAsync();
    }
}
