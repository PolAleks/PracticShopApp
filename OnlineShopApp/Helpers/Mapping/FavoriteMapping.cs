using OnlineShop.Domain.Entities;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Helpers.Mapping
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
                Products = favorite.Products?.ToViewModels().ToList()
            };
        }
    }
}
