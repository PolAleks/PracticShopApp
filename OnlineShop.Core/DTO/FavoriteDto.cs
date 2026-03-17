namespace OnlineShop.Core.DTO
{
    public class FavoriteDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public IReadOnlyCollection<ProductDto> Products { get; set; } = [];
    }
}
