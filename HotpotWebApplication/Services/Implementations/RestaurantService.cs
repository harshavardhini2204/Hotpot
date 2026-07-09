using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Implementations;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Restaurant?> GetRestaurantByOwnerAsync(int userId)
        {
            return await _repository
                .GetRestaurantByOwnerAsync(userId);
        }

        public async Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant)
        {
            await _repository.AddAsync(restaurant);
            await _repository.SaveAsync();
            return restaurant;
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            await _repository.UpdateAsync(restaurant);
            await _repository.SaveAsync();

        }
        public async Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string keyword)
        {
            return await _repository.SearchRestaurantsAsync(keyword);
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);

            if (restaurant == null)
                return false;

            await _repository.DeleteAsync(restaurant);
            await _repository.SaveAsync();

            return true;
        }
    }
}
