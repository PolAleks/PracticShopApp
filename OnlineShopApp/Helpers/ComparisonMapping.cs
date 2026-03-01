using OnlineShop.Db.Models;
using OnlineShopApp.Models;

namespace OnlineShopApp.Helpers
{
    public static class ComparisonMapping
    {
        public static ComparisonViewModel ToViewModel(this Comparison comparison)
        {
            return new ComparisonViewModel()
            {
                Id = comparison.Id,
                UserId = comparison.UserId,
                Items = comparison.Products?.ToViewModels().ToList()
            };
        }
    }
}
