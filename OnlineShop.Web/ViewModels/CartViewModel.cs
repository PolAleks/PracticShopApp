namespace OnlineShop.Web.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalCost { get; set; }
        public string FormattedTotalCost => TotalCost.ToString("C");
        public int Quantity { get; set; }

        public ICollection<ItemViewModel> Items { get; set; }
        public IEnumerable<ItemIndexedViewModel> CartItemsWithIndex =>
            Items.Select((item, index) => new ItemIndexedViewModel()
            {
                Index = index + 1,
                Item = item
            });
    }
}
