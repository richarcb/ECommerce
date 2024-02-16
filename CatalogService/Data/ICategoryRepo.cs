using CatalogService.Models;


namespace CatalogService.Data
{
    public interface ICategoryRepo
    {
        public IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        void CreateCategory(Category category);

        bool SaveChanges();
        
    }
}
