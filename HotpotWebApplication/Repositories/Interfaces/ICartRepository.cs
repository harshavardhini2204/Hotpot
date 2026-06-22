using HotpotWebApplication.Models;

namespace HotpotWebApplication.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);

        Task<Cart?> GetCartByIdAsync(int cartId);

        Task AddCartAsync(Cart cart);

        Task AddCartItemAsync(CartItem cartItem);
        Task<MenuItem?> GetMenuItemByIdAsync(int menuItemId);


        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);

        Task RemoveCartItemAsync(CartItem cartItem);

        Task SaveAsync();
    }
}
