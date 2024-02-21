using CartService.Models;

namespace CartService.Data
{
    public interface ICartRepository
    {
        public Task AddItemToCart(int userId, CartItem item);
        public void RemoveItemFromCart(int userId, int itemId);
        public Task<Cart> GetCartAsync(int userId);

        public Task<List<Cart>> GetCartsAsync();

        public Task<IEnumerable<CartItem>> GetItemsInCart(int userId);
    }
}
