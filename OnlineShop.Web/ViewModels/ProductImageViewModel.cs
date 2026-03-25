namespace OnlineShop.Web.ViewModels
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
