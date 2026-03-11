using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Repositories
{
    public class OrdersRepository(DatabaseContext databaseContext) : IOrdersRepository
    {
        public void Add(Order order)
        {
            if (order is not null)
            {
                order.Id = Guid.NewGuid();
                order.CreationDateTime = DateTime.Now;
                order.Status = OrderStatus.Created;

                databaseContext.Orders.Add(order);
                databaseContext.SaveChanges();
            }
        }

        public List<Order> GetAll()
        {
            var result = databaseContext.Orders
                .Include(o => o.DeliveryUser)
                .Include(o => o.Items)
                .ThenInclude(ci => ci.Product)
                .ToList();

            return result;
        }

        public Order? TryGetById(Guid id)
        {
            return databaseContext.Orders
                .Include(o => o.DeliveryUser)
                .Include(o => o.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        public void UpdateStatus(Guid id, OrderStatus newStatus)
        {
            var existingOrder = TryGetById(id);

            if (existingOrder is not null)
            {
                existingOrder.Status = newStatus;
                databaseContext.SaveChanges();
            }
        }
    }
}
