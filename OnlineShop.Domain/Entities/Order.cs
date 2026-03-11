namespace OnlineShop.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public OrderStatus Status { get; set; }

        public Guid DeliveryUserId { get; set; }

        // Навигационные свойства
        public required DeliveryUser DeliveryUser { get; set; }
        public ICollection<CartItem> Items { get; set; } = null!;
    }
}
