namespace OnlineShop.Domain.Entities
{
    public class FavoriteProduct
    {
        public Guid FavoriteId { get; set; }
        public int ProductId { get; set; }

        public required Favorite Favorite { get; set; }
        public required Product Product { get; set; }
    }
}
