namespace OnlineShopApp.Models
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public List<ProductViewModel>? Items { get; set; }
    }
}
