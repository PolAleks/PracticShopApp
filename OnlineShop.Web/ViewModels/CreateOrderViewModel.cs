using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Web.ViewModels
{
    public class CreateOrderViewModel
    {        
        public string UserId { get; set; }
        [Required]
        public DeliveryUserViewModel DeliveryUser { get; set; }
        [AllowNull]
        public List<ItemViewModel> Items { get; set; }
        public decimal TotalCost => Items.Sum(i => i.TotalCost);
        public string FormattedTotalCost => TotalCost.ToString("C");
    }
}
