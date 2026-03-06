using OnlineShop.Db.Models;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Helpers.Mapping
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

        public static IEnumerable<CartItem> ToModels(this IEnumerable<CartItemViewModel> cartItemViewModels)
        {
            return cartItemViewModels.Select(ToModel).ToList();
        }

        public static CartItem ToModel(this CartItemViewModel cartItemViewModel)
        {
            return new CartItem()
            {
                Id= cartItemViewModel.Id,
                Product = cartItemViewModel.Product!.ToDbModel(),
                Quantity = cartItemViewModel.Quantity
            };
        }

        public static CartViewModel ToViewModel(this Cart cart)
        {
            return new CartViewModel() 
            {
                Id = cart.Id,
                Items = cart.Items.ToViewModels()?.ToList(),
                UserId = cart.UserId
            };
        }
    }
}
