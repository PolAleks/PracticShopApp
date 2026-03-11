using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .HasColumnName("cart_item_id");

            builder.Property(ci => ci.Quantity)
                .HasColumnName("quantity")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(ci => ci.ProductId)
                .HasColumnName("product_id");

            builder.Property(ci => ci.CartId)
                .HasColumnName("cart_id");

            builder.Property(i => i.OrderId)
                .HasColumnName("order_id");

            // Настройка связи с Product (один-ко-многим)
            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Не удалять Product при удалении CartItem

            // Настройка связи с Cart (один комногим)
            builder.HasOne(ci => ci.Cart)
                .WithMany(cart => cart.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Restrict); // Удалять CartItem при удалении Cart

            builder.HasOne(ci => ci.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(ci => ci.OrderId)
                .OnDelete(DeleteBehavior.ClientCascade); // Клиентское каскадное удаление

        }
    }
}
