namespace OnlineShop.Core.DTO
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
