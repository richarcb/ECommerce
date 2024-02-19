using CartService.Models;

namespace CartService.Data
{
    public interface ICartRepository
    {
        public void AddItemToCart(int userId, int productId, int quantity);
        public void RemoveItemFromCart(int userId, int productId);
        public Cart GetCart(string userId);

    }
}
