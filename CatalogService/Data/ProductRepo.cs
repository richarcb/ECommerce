using CatalogService.Models;

namespace CatalogService.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public void CreateProduct(int categoryId, Product product)
        {
            if(product == null)
                throw new ArgumentNullException(nameof(product));
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if(category != null)
            {
                category.Products.Add(product);
                product.CategoryId = categoryId;
                _context.Products.Add(product);
            }
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProductsInCategory(int categoryId)
        {
            return _context.Products.Where(c => c.Id == categoryId).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }
}
