using OnlineShop.Db.Models;
using OnlineShop.Web.ViewModels;

namespace OnlineShopApp.Helpers.Mapping
{
    public static class OrderMapping
    {
        public static OrderViewModel? ToViewModel(this Order? order)
        {
            if (order == null) return null;

            return new OrderViewModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                CreationDateTime = order.CreationDateTime,
                DeliveryUser = order.DeliveryUser.ToViewModel(),
                Items = order.Items.ToViewModels().ToList(),
                Status = (OrderStatusViewModel)order.Status
            };
        }

        public static List<OrderViewModel>? ToViewModels(this List<Order>? orders)
        {
            return orders?
                .Where(o => o != null)?
                .Select(o => o.ToViewModel()!)
                .ToList();
        }
    }
}
