using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class ComparisonProductConfiguration : IEntityTypeConfiguration<ComparisonProduct>
    {
        public void Configure(EntityTypeBuilder<ComparisonProduct> builder)
        {
            builder.ToTable("comparison_products");

            builder.HasKey(cp => new { cp.ComparisonId, cp.ProductId });

            builder.Property(cp => cp.ComparisonId)
                .IsRequired()
                .HasColumnName("comparison_id");

            builder.Property(cp => cp.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.HasOne(cp => cp.Comparison)
                .WithMany(c => c.ComparisonProducts)
                .HasForeignKey(cp => cp.ComparisonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.ComparisonProducts)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Явно указываем, что это не отдельная сущность, а связочная таблица
            builder.HasIndex(cp => cp.ComparisonId);
            builder.HasIndex(cp => cp.ProductId);
        }
    }
}
