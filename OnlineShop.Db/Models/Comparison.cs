namespace OnlineShop.Db.Models
{
    public class Comparison
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<ComparisonProduct> ComparisonProducts { get; set; } = new HashSet<ComparisonProduct>();
    }
}
