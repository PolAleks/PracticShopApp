namespace OnlineShopApp.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Product> Items { get; set; }
    }
}
