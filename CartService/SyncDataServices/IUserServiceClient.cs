using CartService.Dtos;

namespace CartService.SyncDataServices
{
    public interface IUserServiceClient
    {
       Task<UserDto> GetUserByIdAsync(int userId);
    }
}
