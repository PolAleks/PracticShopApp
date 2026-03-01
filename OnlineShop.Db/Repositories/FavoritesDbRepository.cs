using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class FavoritesDbRepository(DatabaseContext databaseContext) : IFavoritesRepository
    {
        public void Add(Product product, string userId)
        {
            var existingFavorite = TryGetByUserId(userId);

            if (existingFavorite == null)
            {
                Favorite favorite = new() 
                {
                    UserId = userId, 
                    Products = [product] 
                };
                
                databaseContext.Favorites.Add(favorite);
            }
            else
            {
                var existingProductInFavorite = existingFavorite.Products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProductInFavorite == null)
                {
                    existingFavorite.Products.Add(product);
                }
            }

            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var existingFavorite = TryGetByUserId(userId);

            if(existingFavorite != null)
            {
                databaseContext.Favorites.Remove(existingFavorite);
                databaseContext.SaveChanges();
            }
        }

        public void Delete(int productId, string userId)
        {
            Favorite? existingFavorite = TryGetByUserId(userId);

            var existingProductInFavorite = existingFavorite?.Products.FirstOrDefault(p => p.Id == productId);

            if(existingProductInFavorite != null)
            {
                existingFavorite!.Products.Remove(existingProductInFavorite);
                databaseContext.SaveChanges();
            }
        }

        public Favorite? TryGetByUserId(string userId)
        {
            return databaseContext.Favorites
                .Include(favorite => favorite.Products)
                .FirstOrDefault(favorite => favorite.UserId == userId);
        }
    }
}
