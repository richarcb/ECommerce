using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Dtos;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository, IMapper mapper) 
        {
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

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            Console.WriteLine("--> Creating user");
            var userExists = await _repository.GetUserByNameAsync(userCreateDto.Username);
            
            if(userExists != null)
                return BadRequest("User already exists");

            var userModel = _mapper.Map<User>(userCreateDto);
            _repository.CreateUserAsync(userModel);
            _repository.SaveChanges();
            return Ok(_mapper.Map<UserReadDto>(userModel));

        }
        

    }
}
