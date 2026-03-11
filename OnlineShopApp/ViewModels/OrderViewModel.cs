using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        public List<CartItemViewModel> Items { get; set; }

        [Required]
        public DeliveryUserViewModel DeliveryUser { get; set; }

        [ValidateNever]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public OrderStatusViewModel Status { get; set; } = OrderStatusViewModel.Created;

        public decimal? TotalCost => Items?.Sum(item => item.Cost);

        public int? ItemsQuantity => Items?.Sum(item => item.Quantity);
    }
}
