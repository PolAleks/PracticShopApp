using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public IEnumerable<ProductImageDto> ProductImages { get; set; } = [];
    }

    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public IEnumerable<IFormFile> Images { get; set; } = [];
    }
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public IEnumerable<ProductImageDto> Images { get; set; } = [];
        public IEnumerable<Guid> ImagesToDelete { get; set; } = [];
        public IEnumerable<IFormFile> NewImages { get; set; } = [];
    }
}
