using HotpotWebApplication.Data;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Repositories.Implementations
{
    public class MenuItemRepository:IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
        }

        public Task UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string keyword)
        {
            return await _context.MenuItems
                .Where(m =>
                    m.ItemName.Contains(keyword) ||
                    m.Description.Contains(keyword))
                .ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId && m.IsAvailable)
                .ToListAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
