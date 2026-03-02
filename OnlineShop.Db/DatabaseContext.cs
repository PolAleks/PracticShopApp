using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Configurations;
using OnlineShop.Db.Models;
using System.Reflection;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }

      
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<DeliveryUser> DeliveryUsers { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.FillData();
        }
    }
}
