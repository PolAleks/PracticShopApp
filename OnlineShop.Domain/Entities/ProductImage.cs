namespace OnlineShop.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public Product Product { get; set; } = null!;
    }
}
