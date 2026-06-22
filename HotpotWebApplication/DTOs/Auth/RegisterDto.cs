using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string? Gender {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }=string.Empty;
        [Required]
        [MinLength(6)]
        public string Password {  get; set; } = string.Empty;
        [Required]
        [Phone]
        public string? PhoneNumber {  get; set; }
        [Required]
        public string? Address {  get; set; }
       
        
    }
}
