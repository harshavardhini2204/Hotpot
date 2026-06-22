using HotpotWebApplication.Models.Enums;

namespace HotpotWebApplication.Models
{
    public class Order
    {
        public int OrderId {  get; set; }
        public int UserId {  get; set; }
        public int RestaurantId {  get; set; }
        public DateTime OrderDate {  get; set; }
        public string? ShippingAddress {  get; set; }
        public decimal TotalAmount {  get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public DateTime? EstimatedDeliveryTime { get; set; }
        public string? Notes {  get; set; }
        public User? User { get; set; }
        public Restaurant? Restaurant { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Payment>? Payments { get; set; }

    }
}
