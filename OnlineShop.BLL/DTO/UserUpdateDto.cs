namespace OnlineShop.BLL.DTO
{
    public class UserUpdateDto
    {
        public required string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
