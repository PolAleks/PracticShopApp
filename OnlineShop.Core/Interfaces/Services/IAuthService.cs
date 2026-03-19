using Microsoft.AspNetCore.Identity;
using OnlineShop.Core.DTO.User;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<SignInResult> LoginAsync(UserLoginDto userLoginDto);
        Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto);
        Task LogoutAsync();
    }
}
