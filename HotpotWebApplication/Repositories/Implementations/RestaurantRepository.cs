using HotpotWebApplication.Data;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Repositories.Implementations
{
    public class RestaurantRepository:IRestaurantRepository
    {
        private readonly ApplicationDbContext _context;
        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
        }

        public Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string keyword)
        {
            return await _context.Restaurants
                .Where(r =>
                    r.RestaurantName.Contains(keyword) ||
                    r.Description.Contains(keyword))
                .ToListAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
