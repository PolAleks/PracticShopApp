using OnlineShop.Domain.Entities;

namespace OnlineShop.Core.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryUserDto DeliveryUser { get; set; } = new();
        public List<ItemDto> Items { get; set; } = [];
    }

    public class CreateOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public DeliveryUserDto DeliveryUser { get; set; } = new(); 
    }
}
