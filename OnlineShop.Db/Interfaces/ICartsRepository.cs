using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Interfaces
{
    public interface ICartsRepository
    {
        Cart? TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Subtract(int productId, string userId);
        void Clear(string userId);
    }
}
