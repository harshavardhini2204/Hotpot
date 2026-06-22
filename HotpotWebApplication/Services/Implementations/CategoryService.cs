using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return category;
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            await _repository.SaveAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            await _repository.DeleteAsync(category);
            await _repository.SaveAsync();

            return true;
        }
    }
}
