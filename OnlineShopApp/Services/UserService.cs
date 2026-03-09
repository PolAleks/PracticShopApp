using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Models.IdentityEntities;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Services
{
    public class UserService(UserManager<ApplicationUser> userManager) : IUserService
    {
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel changePassword)
        {
            var user = await userManager.FindByIdAsync(changePassword.Id.ToString());
            
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не существует!"
                });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var password = changePassword.Password;

            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> ChangeRoleAsync(ChangeRoleViewModel changeRole)
        {
            var user = await userManager.FindByIdAsync(changeRole.Id.ToString());

            if (user == null)
            {
                IdentityResult.Failed(new IdentityError()
                {
                    Description = "Такой пользователь не существует"
                });
            }

            var roles = await userManager.GetRolesAsync(user!);

            foreach (var role in roles)
            {
                var removeResult = await userManager.RemoveFromRoleAsync(user!, role);
                if (!removeResult.Succeeded) return removeResult;
            }

            return await userManager.AddToRoleAsync(user!, changeRole.Role);
        }

        public async Task<IdentityResult> CreateUserAsync(UserViewModel userViewModel)
        {
            ApplicationUser user = new()
            {
                UserName = userViewModel.Login,
                Email = userViewModel.Login,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                PhoneNumber = userViewModel.Phone
            };

            await userManager.AddToRoleAsync(user, BaseTypeRole.User.ToString());

            return await userManager.CreateAsync(user, userViewModel.Password!);
        }

        public async Task<List<UserViewModel>?> GetAllAsync()
        {
            return await userManager.Users
                .Select(u => new UserViewModel
            {
                Id = u.Id,
                Login = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Phone = u.PhoneNumber
            })
                .ToListAsync();
        }

        public async Task<UserViewModel?> TryGetByIdAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if(user == null) return null;

            var roles = await userManager.GetRolesAsync(user);

            return new UserViewModel()
            {
                Id = user.Id,
                Login = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Role = string.Join(",", roles),
                CreationDateTime = user.CreationDateTime                
            };
        }

        public async Task<UserViewModel?> TryGetByLoginAsync(string login)
        {
            var user = await userManager.FindByNameAsync(login);
            
            if (user == null) return null;

            return new UserViewModel()
            {
                Login = user.UserName
            };
        }

        public async Task<IdentityResult> UpdateUserAsync(EditUserViewModel user)
        {
            ApplicationUser? applicationUser = await userManager.FindByIdAsync(user.Id);

            if (applicationUser is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не найден!"
                });
            }

            applicationUser.FirstName = user.FirstName;
            applicationUser.LastName = user.LastName;
            applicationUser.PhoneNumber = user.Phone;

            return await userManager.UpdateAsync(applicationUser);
        }

        public async Task<IdentityResult> DeleteUserAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не найден!"
                });
            }

            return await userManager.DeleteAsync(user);
        }
    }
}
