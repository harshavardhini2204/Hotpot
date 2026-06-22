namespace HotpotWebApplication.DTOs.Cart
{
    public class AddCartItemDto
    {
        public int CartId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
