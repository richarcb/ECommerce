using AutoMapper;
using CatalogService.Dtos;
using CatalogService.Models;
namespace CatalogService.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
