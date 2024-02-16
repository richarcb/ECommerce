using AutoMapper;
using CatalogService.Dtos;
using CatalogService.Models;

namespace CatalogService.Profiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }
    }
}
