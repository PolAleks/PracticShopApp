namespace OnlineShop.Db.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Cost { get; set; }
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }

        // Навигационные свойства
        public ICollection<CartItem> CartItems { get; set; } = [];
        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<Comparison> Comparisons { get; set; } = [];
        public ICollection<ComparisonProduct> ComparisonProducts { get; set; } = new HashSet<ComparisonProduct>();
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new HashSet<FavoriteProduct>();
    }
}
