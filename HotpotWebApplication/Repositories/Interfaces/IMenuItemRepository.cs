using HotpotWebApplication.Models;

namespace HotpotWebApplication.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(int id);
        Task AddAsync(MenuItem menuItem);
        Task UpdateAsync(MenuItem menuItem);
        Task DeleteAsync(MenuItem menuItem);
        Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string keyword);
        Task SaveAsync();
    }
}
