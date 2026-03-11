using OnlineShop.Db.Models;
using OnlineShop.Web.ViewModels;

namespace OnlineShopApp.Helpers.Mapping
{
    public static class DeliveryUserMapping
    {
        public static DeliveryUser ToDbModel(this DeliveryUserViewModel deliveryUserViewModel)
        {
            return new DeliveryUser()
            {
                Id = deliveryUserViewModel.Id,
                Name = deliveryUserViewModel.Name,
                Address = deliveryUserViewModel.Address,
                Phone = deliveryUserViewModel.Phone,
                Date = deliveryUserViewModel.Date,
                Comment = deliveryUserViewModel.Comment,
            };
        }

        public static DeliveryUserViewModel ToViewModel(this DeliveryUser deliveryUser)
        {
            return new DeliveryUserViewModel()
            {
                Id = deliveryUser.Id,
                Name = deliveryUser.Name,
                Address = deliveryUser.Address,
                Phone = deliveryUser.Phone,
                Date = deliveryUser.Date,
                Comment = deliveryUser.Comment,
            };
        }
    }
}
