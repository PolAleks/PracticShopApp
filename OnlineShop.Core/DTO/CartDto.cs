namespace OnlineShop.Core.DTO
{
    [Serializable]
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }
}