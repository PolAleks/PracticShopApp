using OnlineShop.Db.Models;
using OnlineShopApp.Models;

namespace OnlineShopApp.Helpers.Mapping
{
    public static class ComparisonMapping
    {
        public static ComparisonViewModel? ToViewModel(this Comparison? comparison)
        {
            if(comparison is null)
                return null;

            return new ComparisonViewModel()
            {
                Id = comparison.Id,
                UserId = comparison.UserId,
                Items = comparison.Products?.ToViewModels().ToList()
            };
        }
    }
}
