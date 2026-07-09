using HotpotWebApplication.Models;

namespace HotpotWebApplication.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant?> GetRestaurantByIdAsync(int id);
        Task<Restaurant?> GetRestaurantByOwnerAsync(int userId);
        Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string keyword);
        Task<bool> DeleteRestaurantAsync(int id);
    }
}
