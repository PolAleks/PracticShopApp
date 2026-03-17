using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.BLL.DTO;
using OnlineShop.DAL.Entities;
using OnlineShop.Db.Models;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Services
{
    public class UserService(UserManager<User> userManager,
                             IMapper mapper) : IUserService
    {
        public async Task<IdentityResult> ChangePasswordAsync(string id, string password)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не существует!"
                });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> ChangeRoleAsync(string id, string newRole)
        {
            var user = await userManager.FindByIdAsync(id);

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

            return await userManager.AddToRoleAsync(user!, newRole);
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDto user)
        {
            User newUser = mapper.Map<User>(user);

            newUser.
            //User newUser = new()
            //{
            //    UserName = user.Login,
            //    Email = user.Login,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    PhoneNumber = user.Phone
            //};

            IdentityResult creatingResult = await userManager.CreateAsync(newUser, user.Password);

            if (creatingResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, BaseTypeRole.User.ToString());
            }

            return creatingResult;
        }

        public async Task<List<UserDto>?> GetAllAsync()
        {
            return await userManager.Users
                .Select(u => new UserDto
                {
                    Id = u.Id.ToString(),
                    Login = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Phone = u.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<UserDto?> TryGetByIdAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null) return null;

            var roles = await userManager.GetRolesAsync(user);

            return new UserDto()
            {
                Id = user.Id.ToString(),
                Login = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Role = string.Join(",", roles),
                CreationDateTime = user.CreationDateTime
            };
        }

        public async Task<UserDto?> TryGetByLoginAsync(string login)
        {
            var user = await userManager.FindByNameAsync(login);

            if (user == null) return null;

            return new UserDto()
            {
                Login = user.UserName
            };
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto user)
        {
            User? applicationUser = await userManager.FindByIdAsync(user.Id);

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

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

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
