using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByNameAsync(string username);
        Task CreateUserAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync();   
    }
}
