using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.MenuItem
{
    public class CreateMenuDto
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]

        public string ItemName { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Ingridients { get; set; } = string.Empty;
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Range(0, 100000)]
        public decimal? DiscountPrice { get; set; }

        public int CookingTime { get; set; }

        public string AvailabilityTime { get; set; } = string.Empty;
        public string DietaryType { get; set; } = string.Empty;
        public string TasteInfo { get; set; } = string.Empty;

        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fat { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public bool IsAvailable { get; set; }
    }
}
