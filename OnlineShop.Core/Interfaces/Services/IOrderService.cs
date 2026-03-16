using OnlineShop.Core.DTO;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrderAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid id);
        Task CreateOrderAsync(OrderDto orderDto);
        Task UpdateStatusAsync(Guid id, OrderStatus status);
        
    }
}
