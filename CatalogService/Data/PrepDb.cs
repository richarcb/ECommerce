using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using CatalogService.Models;


namespace CatalogService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        /*
        public int Id { getW; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int StarRating { get; set; }
        public List<Room> Rooms { get; set; }
         */
        private static void SeedData(AppDbContext context)
        {

            if (!context.Categories.Any())
            {
                Console.WriteLine("--> Seeding data");
                context.Categories.AddRange(
                    new Category() { Name="Clothing"},
                    new Category() { Name="Kitchen" },
                    new Category() { Name="Gaming" }
                );
                context.Products.AddRange(
                    new Product() { Name="Sweater", Description="Nice sweater", Price=100, CategoryId=1},
                    new Product() { Name="Trousers", Description="Nice trousers", Price = 150, CategoryId=1},
                    new Product() { Name="Spoon 20x", Description="Nice spoons", Price=40, CategoryId=2 },
                    new Product() { Name="Mix master", Description="Nice mix master", Price=250, CategoryId=2 },
                    new Product() { Name="Headset", Description="Nice headset", Price=120, CategoryId=3 }
                    );
                context.SaveChanges();
                var product = context.Products.FirstOrDefault(u => u.Name == "Sweater");
                if (product!= null)
                {
                    Console.WriteLine(product.Description);
                }
            }

            else
            {
                Console.WriteLine("--> We already have data.");
            }
        }
    }
}
