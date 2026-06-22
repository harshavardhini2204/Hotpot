namespace HotpotWebApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber{ get; set; }
        public string? Address { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
