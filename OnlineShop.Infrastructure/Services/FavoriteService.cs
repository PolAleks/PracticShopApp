using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Services
{
    public class FavoriteService(DatabaseContext context, IMapper mapper) : IFavoriteService
    {
        public async Task AddToFavoriteAsync(int productId, string userId)
        {
            var favorite = await GetOrCreateFavoriteAsync(userId);

            var productIsFavorite = favorite.Products.Any(p => p.Id == productId);

            if (!productIsFavorite)
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product != null)
                {
                    favorite.Products.Add(product);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task ClearFavoriteAsync(string userId)
        {
            var favorite = await GetOrCreateFavoriteAsync(userId);

            if (favorite != null)
            {
                context.Favorites.Remove(favorite);
                await context.SaveChangesAsync();
            }
        }

        public async Task<FavoriteDto> GetByUserIdAsync(string userId)
        {
            var favorite = await GetOrCreateFavoriteAsync(userId);

            var favoriteDto = mapper.Map<FavoriteDto>(favorite);

            return favoriteDto;
        }


        public async Task RemoveFromFavoriteAsync(int productId, string userId)
        {
            var favorite = await GetOrCreateFavoriteAsync(userId);

            var existingProduct = favorite.Products?.FirstOrDefault(p => p.Id == productId);

            if (existingProduct != null)
            {
                favorite.Products!.Remove(existingProduct);
                await context.SaveChangesAsync();
            }
        }

        public async Task MergeFavoriteAsync(string sourceUserName, string destinationUserName)
        {
            var hasData = await HasDataAsync(sourceUserName);

            if (!hasData) return;

            var anonymousFavorite = await GetOrCreateFavoriteAsync(sourceUserName);

            var userFavorite = await GetOrCreateFavoriteAsync(destinationUserName);
            
            foreach (var product in anonymousFavorite.Products)
            {
                var userProduct = userFavorite.Products.FirstOrDefault(p => p.Id == product.Id);
                if (userProduct == null)
                {
                    userFavorite.Products.Add(product);
                }
            }
            context.Favorites.Remove(anonymousFavorite);

            await context.SaveChangesAsync();
        }        

        private async Task<Favorite> GetOrCreateFavoriteAsync(string userId)
        {
            var favorite = await context.Favorites
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.UserId == userId);

            if (favorite == null)
            {
                favorite = new()
                {
                    UserId = userId,
                    Products = []
                };
                context.Favorites.Add(favorite);
                await context.SaveChangesAsync();
            }
            else
            {
                favorite.Products ??= [];
            }

            return favorite;
        }

        private async Task<bool> HasDataAsync(string sourceUserName)
        {
            return await context.Favorites
                .Where(f => f.UserId == sourceUserName)
                .Select(f => f.Products.Any())
                .FirstOrDefaultAsync();
        }
    }
}
