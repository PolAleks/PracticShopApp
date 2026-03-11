using OnlineShop.Domain.Entities;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Helpers.Mapping
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
