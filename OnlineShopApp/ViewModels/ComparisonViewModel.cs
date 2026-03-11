using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.ViewModels
{
    public class ComparisonViewModel
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public List<ProductViewModel>? Items { get; set; }
    }
}
