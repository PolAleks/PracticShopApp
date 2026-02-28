namespace OnlineShop.Db.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        // Внешние ключи
        public Guid CartId { get; set; }
        public int ProductId { get; set; }

        // Навигационные свойства
        public required Product Product { get; set; }
        public Cart? Cart { get; set; }
    }
}