using AutoMapper;
using UserService.Models;
using UserService.Dtos;

namespace UserService.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile() 
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
