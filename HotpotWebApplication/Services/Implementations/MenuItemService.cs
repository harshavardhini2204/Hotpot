using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class MenuItemService:IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        public MenuItemService(IMenuItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem)
        {
            await _repository.AddAsync(menuItem);
            await _repository.SaveAsync();
            return menuItem;
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            await _repository.UpdateAsync(menuItem);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<MenuItem>> SearchMenuItemsAsync(string keyword)
        {
            return await _repository.SearchMenuItemsAsync(keyword);
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menu = await _repository.GetByIdAsync(id);

            if (menu == null)
                return false;

            await _repository.DeleteAsync(menu);
            await _repository.SaveAsync();

            return true;
        }
    }
}
