using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Order
{
    public class PlaceOrderDto
    {
        [Required]
        public int UserId {  get; set; }
        
        [Required]
        [StringLength(100)]
        public string? ShippingAddress {  get; set; }
        [Required]
        public string? Notes {  get; set; }
        public string? CouponCode { get; set; }
    }
}
