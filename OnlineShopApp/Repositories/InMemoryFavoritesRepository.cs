using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryFavoritesRepository : IFavoritesRepository
    {
        private readonly List<Favorite> _favorites = [];
        public void Add(Product product, string userId)
        {
            var favorite = TryGetByUserId(userId);
            if (favorite is null)
            {
                _favorites.Add(new Favorite
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = [product]
                });
            }
            else
            {
                var existingFavoriteItem = favorite.Items.FirstOrDefault(item => item.Id == product.Id);

                if (existingFavoriteItem is null)
                {
                    favorite.Items.Add(product);
                }
            }
        }

        public void Clear(string userId)
        {
            var favorite = TryGetByUserId(userId);

            if (favorite is not null)
            {
                _favorites.Remove(favorite);
            }
        }

        public void Delete(int productId, string userId)
        {
            var favorite = TryGetByUserId(userId);

            var existingFavoriteItem = favorite?.Items.FirstOrDefault(item => item.Id == productId);

            if(existingFavoriteItem is not null)
            {
                favorite!.Items.Remove(existingFavoriteItem);
            }
        }

        public Favorite? TryGetByUserId(string userId)
        {
            return _favorites.FirstOrDefault(favorite => favorite.UserId == userId);
        }
    }
}
