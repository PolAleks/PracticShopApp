using OnlineShop.Db.Models;
using OnlineShopApp.Models;

namespace OnlineShopApp.Helpers.Mapping
{
    public static class FavoriteMapping
    {
        public static FavoriteViewModel? ToViewModel(this Favorite? favorite)
        {
            if(favorite is null)
                return null;

            return new FavoriteViewModel()
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                Items = favorite.Products?.ToViewModels().ToList()
            };
        }
    }
}
