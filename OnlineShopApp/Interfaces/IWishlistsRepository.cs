using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IWishlistsRepository
    {
        Wishlist? TryGetWishlistByUserId(string userId);
        void Add(Product product, string userId);
        void Remove(Product product, string userId);
        void Clear(string userId);
    }
}
