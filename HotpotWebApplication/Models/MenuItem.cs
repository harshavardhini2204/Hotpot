namespace HotpotWebApplication.Models
{
    public class MenuItem
    {
        public int MenuItemId {  get; set; }
        public int RestaurantId {  get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? Description {  get; set; }
        public string? Ingridients {  get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice {  get; set; }
        public int? CookingTime { get; set; }
        public string? AvailabilityTime {  get; set; }
        public string? DietaryType {  get; set; }
        public string? TasteInfo {  get; set; }
        public int? Calories { get; set; }
        public decimal? Protein {  get; set; }
        public decimal? Carbs {  get; set; }
        public decimal? Fat { get; set; }
        public string? ImageUrl {  get; set; }
        public bool IsAvailable {  get; set; }
        public DateTime? CreatedDate{get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Category? Category { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }



            

    }
}
