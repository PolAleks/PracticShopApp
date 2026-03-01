using OnlineShop.Db.Models;
using OnlineShopApp.Models;

namespace OnlineShopApp.Helpers
{
    public static class FavoriteMapping
    {
        public static FavoriteViewModel ToViewModel(this Favorite favorite)
        {
            return new FavoriteViewModel()
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                Items = favorite.Products?.ToViewModels().ToList()
            };
        }
    }
}
