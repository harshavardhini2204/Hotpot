using HotpotWebApplication.DTOs.User;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();

            return users.Select(user => new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role.ToString()
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
                return false;

            user.FullName = dto.FullName;

            user.PhoneNumber = dto.PhoneNumber;

            user.Address = dto.Address;

            await _repository.UpdateUserAsync(user);

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
                return false;

            await _repository.DeleteUserAsync(id);

            return true;
        }
    }
}
