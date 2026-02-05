using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IOrdersRepository
    {
        void Add(Order order);
    }
}
