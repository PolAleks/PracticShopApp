using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired()
                .HasColumnName("order_id");

            builder.Property(o => o.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder.Property(o => o.CreationDateTime)
                .IsRequired()
                .HasColumnName("creation_date_time");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.HasOne(o => o.DeliveryUser)
                .WithOne(du => du.Order)
                .HasForeignKey<Order>(o => o.DeliveryUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
