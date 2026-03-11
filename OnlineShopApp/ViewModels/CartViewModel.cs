using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItemViewModel>? Items { get; set; }
        public decimal? TotalCost => Items?.Sum(item => item.Cost);
        public int? Quantity => Items?.Sum(item => item.Quantity);
    }
}
