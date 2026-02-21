namespace OnlineShopApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public Role? Role { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
