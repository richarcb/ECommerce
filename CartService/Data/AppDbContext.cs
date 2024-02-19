using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CartId);
        }
    }
}
