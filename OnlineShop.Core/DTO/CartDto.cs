namespace OnlineShop.Core.DTO
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IReadOnlyCollection<ItemDto> Items { get; set; } = [];
    }
}