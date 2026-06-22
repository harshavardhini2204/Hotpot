using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Restaurant
{
    public class UpdateRestaurantDto
    {
        [Required]
        [StringLength(100)]
        public string RestaurantName { get; set; } = string.Empty;
        [Required]
        public string? Location { get; set; }
        [Required]
        [Phone]
        public string? ContactNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
        public string? LogUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
