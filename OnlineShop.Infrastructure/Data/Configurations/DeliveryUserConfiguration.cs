using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data.Configurations
{
    public class DeliveryUserConfiguration : IEntityTypeConfiguration<DeliveryUser>
    {
        public void Configure(EntityTypeBuilder<DeliveryUser> builder)
        {
            builder.ToTable("delivery_user");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasColumnName("delivery_user_id");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(u => u.Address)
                .IsRequired()
                .HasColumnName("address");

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasColumnName("phone");

            builder.Property(u => u.Date)
                .HasColumnName("date");

            builder.Property(u => u.Comment)
                .HasColumnName("comment");

            builder.HasOne(u => u.Order)
                .WithOne(o => o.DeliveryUser)
                .HasForeignKey<Order>(o => o.DeliveryUserId)
                .OnDelete(DeleteBehavior.Restrict);               
        }
    }
}
