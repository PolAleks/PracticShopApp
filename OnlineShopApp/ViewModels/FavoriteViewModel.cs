namespace OnlineShop.Web.ViewModels
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public List<ProductViewModel>? Items { get; set; }
    }
}
