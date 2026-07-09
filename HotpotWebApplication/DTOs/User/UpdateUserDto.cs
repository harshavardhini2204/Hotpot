using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string? Address { get; set; }
    }
}
