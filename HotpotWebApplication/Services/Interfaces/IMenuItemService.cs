using HotpotWebApplication.Models;

namespace HotpotWebApplication.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string keyword);
        Task<bool> DeleteMenuItemAsync(int id);
    }
}
