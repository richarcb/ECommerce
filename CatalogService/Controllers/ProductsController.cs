using AutoMapper;
using CatalogService.Data;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Models;
using CatalogService.Dtos;
namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repository;

        public ProductsController(IMapper mapper, IProductRepo repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            return Ok(_repository.GetProductsInCategory(categoryId));
        }

        [HttpPost("{categoryId}")]
        public ActionResult<ProductReadDto> CreateProductInCategory(int categoryId, ProductCreateDto productCreateDto)
        {
            if (!_repository.CategoryExists(categoryId))
                return NotFound();

            var product = _mapper.Map<Product>(productCreateDto);
            _repository.CreateProduct(categoryId, product);
            _repository.SaveChanges();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }
    }
}
