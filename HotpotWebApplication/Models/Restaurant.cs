namespace HotpotWebApplication.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email {  get; set; }
        public string? Description {  get; set; }
        public string? LogUrl {  get; set; }
        public decimal Rating { get; set; } = 4.0m;

        public int DeliveryTime { get; set; } = 30;

        public decimal CostForOne { get; set; } = 250;

        public string CoverImageUrl { get; set; } = string.Empty;
        public bool? IsActive {  get; set; }
        public DateTime CreatedDate {  get; set; }
        public User? User { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
