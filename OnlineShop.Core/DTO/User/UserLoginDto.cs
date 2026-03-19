namespace OnlineShop.Core.DTO.User
{
    public class UserLoginDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsRememberMe { get; set; }
    }
}
