namespace OnlineShop.BLL.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
