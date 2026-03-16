using OnlineShop.Domain.Entities;

namespace OnlineShop.Core.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryUserDto DeliveryUser { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }

    public class CreationOrderDto
    {
        public string UserId { get; set; }
        public DeliveryUserDto DeliveryUser { get; set; }   
    }
}
