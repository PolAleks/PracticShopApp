using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class FavoriteProductConfiguration : IEntityTypeConfiguration<FavoriteProduct>
    {
        public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
        {
            builder.ToTable("favorite_products");

            builder.HasKey(fp => new { fp.FavoriteId, fp.ProductId });

            builder.Property(fp => fp.FavoriteId)
                .IsRequired()
                .HasColumnName("favorite_id");

            builder.Property(fp => fp.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.HasOne(fp => fp.Favorite)
                .WithMany(f => f.FavoriteProducts)
                .HasForeignKey(fp => fp.FavoriteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fp => fp.Product)
                .WithMany(p => p.FavoriteProducts)
                .HasForeignKey(fp => fp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(fp => fp.FavoriteId);
            builder.HasIndex(fp => fp.ProductId);
        }
    }
}
