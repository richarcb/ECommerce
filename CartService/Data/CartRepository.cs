using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddItemToCart(int userId, CartItem item)
        {
            // Check if the cart exists for the user
            var cart = await _context.Carts
                .Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);

            // If the cart doesn't exist, create a new one
            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                _context.Carts.Add(cart);
            }

            // Associate the cart item with the cart
            item.CartId = cart.Id;

            // Add the cart item to the database
            _context.CartItems.Add(item);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartAsync(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItemsInCart(int userId)
        {
            return await _context.CartItems.Where(c => c.CartId == userId).ToListAsync();
        }

        public void RemoveItemFromCart(int userId, int itemId)
        {
            throw new NotImplementedException();
        }




    }
}
