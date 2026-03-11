using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        void Add(Order order);
        List<Order> GetAll();
        Order? TryGetById(Guid id);
        void UpdateStatus(Guid id, OrderStatus status);
    }
}
