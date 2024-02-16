using AutoMapper;
using CatalogService.Data;
using CatalogService.Dtos;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Models;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _repository;

        public CategoryController(ICategoryRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet("{categoryId}")]
        public ActionResult<CategoryReadDto> GetCategory(int categoryId) 
        {
            Console.WriteLine("--> Getting category by id.");
            var categoryItem = _repository.GetCategoryById(categoryId);
            if(categoryItem == null)
                return NotFound();
            return Ok(_mapper.Map<CategoryReadDto>(categoryItem));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryReadDto>> GetCategories()
        {
            Console.WriteLine("--> Getting all categories");
            var categories = _repository.GetCategories();
            if (categories == null)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));
        }

        [HttpPost]
        public ActionResult<CategoryReadDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            Console.WriteLine("--> Creating category");
            if(categoryCreateDto == null)
                return NotFound();
            var category = _mapper.Map<Category>(categoryCreateDto);
            _repository.CreateCategory(category);
            return Ok(_mapper.Map<CategoryReadDto>(category));
        }
    }
}
