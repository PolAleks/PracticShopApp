using Microsoft.AspNetCore.Identity;
using OnlineShop.Core.DTO.User;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Services
{
    public class AuthService(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             ICurrentUserService currentUser,
                             IUserDataMergeService userDataMerge) : IAuthService
    {
        public async Task<SignInResult> LoginAsync(UserLoginDto userLoginDto)
        {
            var anonymousUser = currentUser.UserName;

            var result = await signInManager.PasswordSignInAsync(userName: userLoginDto.Login,
                                                                 password: userLoginDto.Password,
                                                                 isPersistent: userLoginDto.IsRememberMe,
                                                                 lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await userDataMerge.MergeAnonymousDataAsync(anonymousUser, currentUser.UserName);
            }

            return result;
        }


        public async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            User user = new()
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.UserName,
                PhoneNumber = userRegisterDto.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, userRegisterDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, BaseTypeRole.User.ToString());

                var anonymousUser = currentUser.UserName;

                await signInManager.SignInAsync(user, isPersistent: false);

                await userDataMerge.MergeAnonymousDataAsync(anonymousUser, currentUser.UserName);
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
