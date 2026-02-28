using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class CartConfigurarion : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts");

            builder.HasKey(cart => cart.Id);

            builder.Property(cart => cart.Id)
                .HasColumnName("cart_id");

            builder.Property(cart => cart.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            // Настройка связи с CartItem (один-ко-многим)
            builder.HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
