using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class ComparsionConfiguration : IEntityTypeConfiguration<Comparison>
    {
        public void Configure(EntityTypeBuilder<Comparison> builder)
        {
            builder.ToTable("comparison");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("comparison_id");

            builder.Property(c => c.UserId)
                .HasColumnName("user_id");

            builder.HasMany(c => c.Products)
                .WithMany(p => p.Comparisons);
        }
    }
}
