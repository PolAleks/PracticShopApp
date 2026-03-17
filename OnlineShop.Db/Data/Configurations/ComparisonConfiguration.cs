using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class ComparisonConfiguration : IEntityTypeConfiguration<Comparison>
    {
        public void Configure(EntityTypeBuilder<Comparison> builder)
        {
            builder.ToTable("comparisons");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("comparison_id");

            builder.Property(c => c.UserId)
                .HasColumnName("user_id");

            // Настройка связи многие-ко-многим с Product через ComparisonProduct
            builder.HasMany(c => c.Products)
                .WithMany(p => p.Comparisons)
                .UsingEntity<ComparisonProduct>(
                    j => j.HasOne(cp => cp.Product)
                          .WithMany(p => p.ComparisonProducts)
                          .HasForeignKey(cp => cp.ProductId)
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne(cp => cp.Comparison)
                          .WithMany(c => c.ComparisonProducts) // предполагаем, что это свойство есть
                          .HasForeignKey(cp => cp.ComparisonId)
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("comparison_products");
                        j.HasKey(cp => new { cp.ComparisonId, cp.ProductId });
                        j.Property(cp => cp.ComparisonId).HasColumnName("comparison_id");
                        j.Property(cp => cp.ProductId).HasColumnName("product_id");
                    });
        }
    }
}
