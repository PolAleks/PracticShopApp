using OnlineShop.Db.Models;
using OnlineShopApp.Models;

namespace OnlineShopApp.Helpers
{
    public static class CartMapping
    {
        public static CartItemViewModel ToViewModel(this CartItem item)
        {
            return new CartItemViewModel()
            {
                Id = item.Id,
                Product = item.Product.ToViewModel(),
                Quantity = item.Quantity
            };
        }

        public static IEnumerable<CartItemViewModel> ToViewModels(this IEnumerable<CartItem> items)
        {
            return items.Select(ToViewModel);
        }

        public static CartViewModel ToViewModel(this Cart cart)
        {
            return new CartViewModel() 
            {
                Id = cart.Id,
                Items = cart.Items?.ToViewModels().ToList(),
                UserId = cart.UserId
            };
        }
    }
}
