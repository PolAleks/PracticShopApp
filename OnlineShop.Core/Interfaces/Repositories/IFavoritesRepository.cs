using OnlineShop.Domain.Entities;

namespace OnlineShop.Core.Interfaces.Repositories
{
    public interface IFavoritesRepository
    {
        Favorite? TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Delete(int productId, string userId);
        void Clear(string userId);
    }
}
