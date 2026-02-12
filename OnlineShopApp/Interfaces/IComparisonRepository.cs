using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IComparisonRepository
    {
        Comparison? TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Delete(int productId, string userId);
        void Clear(string userId);
    }
}
