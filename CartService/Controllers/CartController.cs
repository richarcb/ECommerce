using AutoMapper;
using CartService.Data;
using CartService.Dtos;
using CartService.Models;
using CartService.SyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _repository;
        private readonly IUserServiceClient _userServiceClient;
        private readonly ICatalogServiceClient _catalogServiceClient;

        public CartController(IMapper mapper, ICartRepository repository, IUserServiceClient userServiceClient, ICatalogServiceClient catalogServiceClient)
        {
            _mapper = mapper;
            _repository = repository;
            _userServiceClient = userServiceClient;
            _catalogServiceClient = catalogServiceClient;
        }

        [HttpGet("user/{userId}")] 
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            Console.WriteLine("--> Getting user from userid");
            var user = await _userServiceClient.GetUserByIdAsync(userId);
            if(user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("user/{userId}/product/{productId}")]
        public async Task<ActionResult> AddItemToCart(int userId, int productId)
        {
            ProductDto product = await _catalogServiceClient.GetProductByIdAsync(productId);
            Cart cart = await _repository.GetCartAsync(userId);
            if (cart == null)
            {
                // If the cart doesn't exist, create a new one
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
            }
            var cartItem = new CartItem { ProductId = productId, Name = product.Name, Price = product.Price, Quantity = 1, CartId=userId};

            await _repository.AddItemToCart(userId, cartItem);

            _repository.AddItemToCart(userId, cartItem);
            Console.WriteLine($"Added item to cart: {cartItem.Name}");
            return Ok(cartItem);
        }

        [HttpGet("cartItems/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartItemsInCartr(int userId)
        {
            IEnumerable<CartItem> cartItems = await _repository.GetItemsInCart(userId);
            if (cartItems == null)
                return NotFound();

            return Ok(cartItems);
        }
    }
}
