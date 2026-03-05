using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Configurations;
using OnlineShop.Db.Models;
using OnlineShop.Db.Models.IdentityEntities;
using System.Reflection;

namespace OnlineShop.Db
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }

      
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<DeliveryUser> DeliveryUsers { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.FillData();
        }
    }
}
