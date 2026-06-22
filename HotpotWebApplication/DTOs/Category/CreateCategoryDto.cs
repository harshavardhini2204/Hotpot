using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
