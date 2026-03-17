namespace OnlineShop.Core.DTO
{
    public class DeliveryUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string Comment { get; set; } = string.Empty;
        public OrderDto Order { get; set; } = new();
    }
}
