namespace OnlineShopApp.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ProductViewModel> Items { get; set; }
    }
}
