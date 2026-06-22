namespace HotpotWebApplication.Models
{
    public class Cart
    {
        public int CartId {  get; set; }
        public int UserId {  get; set; }
        public DateTime CreatedDate { get; set; }
        public User? User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

    }
}
