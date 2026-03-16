using OnlineShop.Domain.Entities;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Helpers.Mapping
{
    public static class CartMapping
    {
        public static ItemViewModel ToViewModel(this Item item)
        {
            return new ItemViewModel()
            {
                Id = item.Id,
                //Product = item.Product.ToViewModel(),
                Quantity = item.Quantity
            };
        }

        public static IEnumerable<ItemViewModel> ToViewModels(this IEnumerable<Item> items)
        {
            return items.Select(ToViewModel);
        }

        public static IEnumerable<Item> ToModels(this IEnumerable<ItemViewModel> cartItemViewModels)
        {
            return cartItemViewModels.Select(ToModel).ToList();
        }

        public static Item ToModel(this ItemViewModel cartItemViewModel)
        {
            return new Item()
            {
                Id = cartItemViewModel.Id,
                //Product = cartItemViewModel.Product!.ToDbModel(),
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
