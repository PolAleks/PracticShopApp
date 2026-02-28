namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }

        // Навигационные свойства
        public ICollection<CartItem>? Items { get; set; }
    }
}
