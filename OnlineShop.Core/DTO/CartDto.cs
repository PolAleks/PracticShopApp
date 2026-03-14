namespace OnlineShop.Core.DTO
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItemDto> Items { get; set; }
        public decimal TotalCost => Items.Sum(i => i.TotalCost);
        public int Quantity => Items.Sum(i => i.Quantity);
    }

    public class CartItemDto
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string FormattedPrice => ProductPrice.ToString("C");
        public int Quantity { get; set; }
        public decimal TotalCost => ProductPrice * Quantity;
        public string FormattedTotal => TotalCost.ToString("C");
    }
}