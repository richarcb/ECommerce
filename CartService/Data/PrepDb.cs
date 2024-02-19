using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using CartService.Models;
using CartService.Data;


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

            if (!context.Carts.Any())
            {
                Console.WriteLine("--> Seeding data");
                context.Carts.AddRange(
                    new Cart() { UserId=1},
                    new Cart() { UserId=2 },
                    new Cart() { UserId=3 }
                );
                context.CartItems.AddRange(
                    new CartItem() { CartId=1, Name="Sweater", Price=100, ProductId=1, Quantity=1},
                    new CartItem() { CartId=1, Name="Trousers", Price=150, ProductId=2, Quantity=1},
                    new CartItem() { CartId=1, Name="Headset", Price=120, ProductId=5, Quantity=1}
                    );
                context.SaveChanges();
                var cartItem= context.CartItems.FirstOrDefault(u => u.Name == "Sweater");
                if (cartItem!= null)
                {
                    Console.WriteLine(cartItem.Name);
                }
            }

            else
            {
                Console.WriteLine("--> We already have data.");
            }
        }
    }
}
