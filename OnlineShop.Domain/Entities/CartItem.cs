namespace OnlineShop.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        // Внешние ключи
        public int ProductId { get; set; }
        public Guid? CartId { get; set; }
        public Guid? OrderId { get; set; }

        // Навигационные свойства
        public required Product Product { get; set; }
        public Cart? Cart { get; set; }
        public Order? Order { get; set; }
    }
}