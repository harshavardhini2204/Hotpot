namespace HotpotWebApplication.Models
{
    public class CartItem
    {
        public int CartItemId {  get; set; }
        public int CartId { get; set; }
        public int MenuItemId {  get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
        public DateTime AddedDate {  get; set; }
        public Cart? Cart {  get; set; }
        public MenuItem? MenuItem { get; set; }

    }
}
