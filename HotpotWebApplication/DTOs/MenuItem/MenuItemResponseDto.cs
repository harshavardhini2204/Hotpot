namespace HotpotWebApplication.DTOs.MenuItem
{
    public class MenuItemResponseDto
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
