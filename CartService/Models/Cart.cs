namespace CartService.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
