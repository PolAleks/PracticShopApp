namespace OnlineShop.Db.Models
{
    public class DeliveryUser
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public DateOnly Date { get; set; }
        public string? Comment { get; set; }

        public Order Order { get; set; }
    }
}
