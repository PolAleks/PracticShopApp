using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface ICartsRepository
    {
        Cart? TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Subtract(Product product, string userId);
    }
}
