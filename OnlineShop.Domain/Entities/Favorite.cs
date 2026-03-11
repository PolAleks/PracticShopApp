namespace OnlineShop.Domain.Entities
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }

        public ICollection<Product> Products { get; set; } = [];
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new HashSet<FavoriteProduct>();
    }
}
