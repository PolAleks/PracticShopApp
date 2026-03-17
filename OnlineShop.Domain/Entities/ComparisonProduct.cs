namespace OnlineShop.Domain.Entities
{
    public class ComparisonProduct
    {
        public Guid ComparisonId { get; set; }
        public int ProductId { get; set; }

        public required Comparison Comparison { get; set; }
        public required Product Product { get; set; }
    }
}
