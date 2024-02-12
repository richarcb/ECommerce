using CatalogService.Models;


namespace CatalogService.Data
{
    public interface ICategoryRepo
    {
        public IEnumerable<Category> GetCategories();
        public IEnumerable<Product> GetProductsInCategory(int categoryId);

        Category GetCategoryById(int categoryId);
        void CreateCategory(Category category);
        
    }
}
