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

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? TryGetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(Guid id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
