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
                _orders.Add(order);
            }
        }
    }
}
