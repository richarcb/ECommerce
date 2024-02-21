using System.Text.Json.Serialization;

namespace CartService.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public List<CartItem> Items { get; set; }
        //public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
