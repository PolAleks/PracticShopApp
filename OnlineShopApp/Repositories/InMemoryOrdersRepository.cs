using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryOrdersRepository : IOrdersRepository
    {
        private readonly List<Order> _orders = [];
        public void Add(Order order)
        {
            if (order is not null)
            {
                order.Id = Guid.NewGuid();
                order.CreationDateTime = DateTime.Now;
                order.DeliveryUser.Id = Guid.NewGuid();

                _orders.Add(order);
            }
        }

        public List<Order> GetAll() => _orders;

        public Order? TryGetById(Guid id) => _orders.FirstOrDefault(o => o.Id == id);

        public void UpdateStatus(Guid id, OrderStatus newStatus)
        {
            var existingOrder = TryGetById(id);

            if (existingOrder is not null)
            {
                existingOrder.Status = newStatus;
            }
        }
    }
}
