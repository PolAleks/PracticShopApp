namespace OnlineShop.Web.ViewModels
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public string FormattedProductPrice => ProductPrice.ToString("C");
        public string FormattedTotalCost => TotalCost.ToString("C");
    }
}
