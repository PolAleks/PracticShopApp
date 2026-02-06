using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryWishlistsRepository : IWishlistsRepository
    {
        private readonly List<Wishlist> _wishlists = [];
        public void Add(Product product, string userId)
        {
            var existingWishlist = TryGetWishlistByUserId(userId);
            if (existingWishlist is null)
            {
                _wishlists.Add(new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = [product]
                });
            }
            else
            {
                var existingItem = existingWishlist.Items.FirstOrDefault(item => item.Id == product.Id);
                if (existingItem is null)
                {
                    existingWishlist.Items.Add(product);
                }
                else
                {
                    existingWishlist.Items.Remove(product);
                }
            }
        }

        public void Clear(string userId)
        {
            var existingWishlist = _wishlists.FirstOrDefault(w => w.UserId == userId);
            if (existingWishlist is not null)
            {
                _wishlists.Remove(existingWishlist);
            }
        }

        public void Remove(Product product, string userId)
        {
            var existingWishlist = TryGetWishlistByUserId(userId);

            if (existingWishlist is not null)
            {
                var isWishlist = existingWishlist.Items.Contains(product);
                if (isWishlist)
                {
                    existingWishlist.Items.Remove(product);
                }
            }
        }

        public Wishlist? TryGetWishlistByUserId(string userId)
        {
            return _wishlists.FirstOrDefault(w => w.UserId == userId);
        }
    }
}
