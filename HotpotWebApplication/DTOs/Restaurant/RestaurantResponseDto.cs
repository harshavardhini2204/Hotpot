namespace HotpotWebApplication.DTOs.Restaurant
{
    public class RestaurantResponseDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
