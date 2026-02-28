using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .HasColumnName("cart_item_id")
                .ValueGeneratedNever(); // Запрет генерации GUID в БД

            builder.Property(ci => ci.Quantity)
                .HasColumnName("quantity")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(ci => ci.CartId)
                .HasColumnName("cart_id")
                .IsRequired();

            builder.Property(ci => ci.ProductId)
                .HasColumnName("product_id");

            // Настройка связи с Product (один-ко-многим)
            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Не удалять Product при удалении CartItem

            // Настройка связи с Cart (один комногим)
            builder.HasOne(ci => ci.Cart)
                .WithMany(cart => cart.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Удалять CartItem при удалении Cart

        }
    }
}
