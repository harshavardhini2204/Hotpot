using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Cart
{
    public class AddToCartDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
