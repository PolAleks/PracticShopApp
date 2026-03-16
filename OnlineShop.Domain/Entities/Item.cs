namespace OnlineShop.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        // Внешние ключи
        public int ProductId { get; set; }
        public Guid? CartId { get; set; }
        public Guid? OrderId { get; set; }

        // Навигационные свойства
        public Product Product { get; set; } = null!;
        public Cart? Cart { get; set; }
        public Order? Order { get; set; }
    }
}