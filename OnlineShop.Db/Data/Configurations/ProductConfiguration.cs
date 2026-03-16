using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .HasColumnName("product_id")
                .ValueGeneratedOnAdd()        // Автоинкремент в PostgreSQL
                .UseIdentityColumn();

            builder.Property(product => product.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(product => product.Cost)
                .HasColumnName("cost")
                .HasColumnType("decimal(10,2)")
                .HasPrecision(10, 2);

            builder.Property(product => product.Description)
                .HasColumnName("description")
                .HasMaxLength(4069);

            builder.Property(product => product.PhotoPath)
                .HasColumnName("photo_path");


            // Настройка связи с CarItem (один-ко-многим)
            builder.HasMany(p => p.Items)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
