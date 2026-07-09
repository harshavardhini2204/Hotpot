using HotpotWebApplication.DTOs.User;
using HotpotWebApplication.Models;

namespace HotpotWebApplication.Services.Interfaces
{
    public interface IUserService
    {
      
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);

        Task<bool> DeleteUserAsync(int id);
    }
}
