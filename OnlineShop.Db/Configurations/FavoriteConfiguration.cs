using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("favorite");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("favorite_id");

            builder.Property(f => f.UserId)
                .HasColumnName("user_id");

            builder.HasMany(f => f.Products)
                .WithMany(p => p.Favorites);                
        }
    }
}
