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
        public bool? IsActive {  get; set; }
        public DateTime CreatedDate {  get; set; }
        public User? User { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
