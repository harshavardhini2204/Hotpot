using HotpotWebApplication.Models;
namespace HotpotWebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
    }
}
