namespace OnlineShop.Core.DTO
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public IReadOnlyCollection<ItemDto> Items { get; set; } = [];
    }
}