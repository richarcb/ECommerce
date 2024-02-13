using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Dtos;
using UserService.AuthService;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository, IMapper mapper, IAuthService authService) 
        {
            _authService = authService;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("id/{userId}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int userId)
        {
            var userItem = await _repository.GetUserByIdAsync(userId);
            if(userItem != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItem));
                
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult<UserReadDto>> GetUserByName(string name)
        {
            var userItem = await _repository.GetUserByNameAsync(name);
            if (userItem != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItem));

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _repository.GetUsersAsync();
            if(users == null)
                return NotFound();
            else
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            Console.WriteLine("--> Creating user");
            var userExists = await _repository.GetUserByNameAsync(userCreateDto.Username);
            
            if(userExists != null)
                return BadRequest("User already exists.");

            var userModel = _mapper.Map<User>(userCreateDto);

            var passwordHash = _authService.HashPassword(userCreateDto.PasswordHash);
            userModel.PasswordHash = passwordHash;
            Console.WriteLine("Passwordhash" + userModel.PasswordHash);

            await _repository.CreateUserAsync(userModel);
            _repository.SaveChanges();
            return Ok(_mapper.Map<UserReadDto>(userModel));

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _repository.GetUserByNameAsync(userLoginDto.Username);
            Console.WriteLine($"--> Logging in with user: {user.Username}");

            if(user == null)
                return NotFound();

            var isPasswordValid = _authService.VerifyPassword(user.PasswordHash, userLoginDto.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized();

            var token = _authService.GenerateToken(user.Username);
            Console.WriteLine("User successfully logged in.");
            return Ok(new { Token = token });
        }

        [HttpGet("validate")]
        public IActionResult ValidateEndpoint()
        {
            string token = Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            token = token.Substring(7);

            if(!_authService.ValidateToken(token))
                return Unauthorized();

            return Ok("Successful validation");
        }
        
    }
}
