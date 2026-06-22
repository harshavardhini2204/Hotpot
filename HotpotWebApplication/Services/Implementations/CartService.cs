using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Cart;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Implementations;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class CartService:ICartService
    {
        private readonly ICartRepository _repository;
        private readonly ApplicationDbContext _context;
        public CartService(ICartRepository repository,ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task AddToCartAsync(AddToCartDto dto)
        {
            var cart = await _repository.GetCartByUserIdAsync(dto.UserId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = dto.UserId,
                    CreatedDate = DateTime.Now
                };

                await _repository.AddCartAsync(cart);
                await _repository.SaveAsync();
            }

            var menuItem =
                await _repository.GetMenuItemByIdAsync(dto.MenuItemId);

            if (menuItem == null)
                throw new Exception("Menu item not found");

            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                MenuItemId = dto.MenuItemId,
                Quantity = dto.Quantity,
                UnitPrice = menuItem.Price,
                AddedDate = DateTime.Now
            };

            await _repository.AddCartItemAsync(cartItem);
            await _repository.SaveAsync();
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _repository
                .GetCartByUserIdAsync(userId);
        }
        public async Task<bool> UpdateQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _repository.GetCartItemByIdAsync(cartItemId);

            if (cartItem == null)
                return false;

            cartItem.Quantity = quantity;

            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _repository.GetCartItemByIdAsync(cartItemId);

            if (cartItem == null)
                return false;

            await _repository.RemoveCartItemAsync(cartItem);
            await _repository.SaveAsync();

            return true;
        }

    }
}
