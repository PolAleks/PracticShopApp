using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("product_image");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Id)
                .HasColumnName("product_image_id");

            builder.Property(pi => pi.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(pi => pi.ImagePath)
                .IsRequired()
                .HasColumnName("image_path");

            builder.Property(pi => pi.IsMain)
                .HasColumnName("is_main")
                .HasDefaultValue(false);

            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pi => new { pi.ProductId, pi.IsMain })
                .IsUnique()
                .HasFilter("\"is_main\" = true");
        }
    }
}
