using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IWishlistsRepository
    {
        Wishlist? TryGetWishkistbyUserId(string userId);
        void Add(int productId);
        void Remove(int productId);
        void Clear(string userId);
    }
}
