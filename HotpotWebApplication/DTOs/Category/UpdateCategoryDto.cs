using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
