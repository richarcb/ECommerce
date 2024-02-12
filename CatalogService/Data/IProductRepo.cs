using CatalogService.Models;

namespace CatalogService.Data
{
    public interface IProductRepo
    {
        bool SaveChanges();
        IEnumerable<Product> GetProductsInCategory(int categoryId);
        Product GetProductById(int id);
        void CreateProduct(int categoryId, Product product);
        bool CategoryExists(int categoryId);

    }
}
