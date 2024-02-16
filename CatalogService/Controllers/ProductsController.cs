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

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<ProductReadDto>> GetProductsByCategory(int categoryId)
        {
            Console.WriteLine("--> Getting all products in category");
            if (!_repository.CategoryExists(categoryId))
                return NotFound();
            var productItems = _repository.GetProductsInCategory(categoryId);
            if(productItems == null)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
        }

        [HttpGet("{productId}")]
        public ActionResult<ProductReadDto> GetProductById(int productId)
        {
            Console.WriteLine("--> Getting product by Id");
            var productItem = _repository.GetProductById(productId);
            if(productItem == null)
                return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(productItem));
        }
        

        [HttpPost("category/{categoryId}")]
        public ActionResult<ProductReadDto> CreateProductInCategory(int categoryId, ProductCreateDto productCreateDto)
        {
            Console.WriteLine("--> Creating product");
            if (!_repository.CategoryExists(categoryId))
                return NotFound();

            var product = _mapper.Map<Product>(productCreateDto);
            _repository.CreateProduct(categoryId, product);
            _repository.SaveChanges();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }
    }
}
