using OnlineShop.Domain.Entities;

namespace OnlineShop.Core.DTO
{
    public class ComparisonDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IReadOnlyCollection<ProductDto> Products { get; set; } = [];
    }
}
