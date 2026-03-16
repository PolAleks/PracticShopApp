using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; } = string.Empty;

        [ValidateNever]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public OrderStatusViewModel Status { get; set; } = OrderStatusViewModel.Created;

        [Required]
        public DeliveryUserViewModel DeliveryUser { get; set; } = null!;

        [ValidateNever]
        public List<ItemViewModel> Items { get; set; } = [];

        [ValidateNever]
        public decimal TotalCost => Items.Sum(i => i.TotalCost);

        [ValidateNever]
        public int TotalQuantity => Items.Sum(i => i.Quantity);

        // Formatted property
        [ValidateNever]
        public string FormattedTotalCost => TotalCost.ToString("C");
        [ValidateNever]
        public string CreationDate { get; set; } = string.Empty;
        [ValidateNever]
        public string CreationTime { get; set; } = string.Empty;


        public IEnumerable<ItemIndexedViewModel> OrderItemsWithIndex =>
            Items.Select((item, index) => new ItemIndexedViewModel()
            {
                Index = index + 1,
                Item = item
            });
    }
}
