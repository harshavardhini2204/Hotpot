using HotpotWebApplication.DTOs.Cart;
using HotpotWebApplication.Models;

namespace HotpotWebApplication.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(AddToCartDto dto);

        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<bool> UpdateQuantityAsync(int cartItemId, int quantity);

        Task<bool> RemoveCartItemAsync(int cartItemId);
    }
}
