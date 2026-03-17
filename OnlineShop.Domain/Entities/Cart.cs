namespace OnlineShop.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }

        // Навигационные свойства
        public ICollection<Item> Items { get; set; } = [];
    }
}
