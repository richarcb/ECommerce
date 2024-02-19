using CatalogService.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CatalogService.Data
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            _context = context;   
        }
        public void CreateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            _context.Add(category);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }
}
