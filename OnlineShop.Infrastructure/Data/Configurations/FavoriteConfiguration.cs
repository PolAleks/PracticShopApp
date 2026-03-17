using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("favorites");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("favorite_id");

            builder.Property(f => f.UserId)
                .HasColumnName("user_id");

            builder.HasMany(f => f.Products)
                .WithMany(p => p.Favorites)
                .UsingEntity<FavoriteProduct>(
                    j => j.HasOne(fp => fp.Product)
                          .WithMany(p => p.FavoriteProducts)
                          .HasForeignKey(fp => fp.ProductId)
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne(fp => fp.Favorite)
                          .WithMany(f => f.FavoriteProducts)
                          .HasForeignKey(fp => fp.FavoriteId)
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("favorite_products");
                        j.HasKey(fp => new { fp.FavoriteId, fp.ProductId });
                        j.Property(fp => fp.FavoriteId).HasColumnName("favorite_id");
                        j.Property(fp => fp.ProductId).HasColumnName("product_id");
                    });
        }
    }
}
