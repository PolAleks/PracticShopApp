using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO.User;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Services
{
    public class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
    {
        public async Task<IdentityResult> ChangePasswordAsync(ChangeUserPasswordDto passwordDto)
        {
            var user = await userManager.FindByIdAsync(passwordDto.Id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не существует!"
                });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var password = passwordDto.Password;

            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> ChangeRoleAsync(ChangeUserRoleDto roleDto)
        {
            var user = await userManager.FindByIdAsync(roleDto.Id);

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

            return await userManager.AddToRoleAsync(user!, roleDto.Role);
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDto registeredUser)
        {
            User user = mapper.Map<User>(registeredUser);

            var createdUserResult = await userManager.CreateAsync(user, registeredUser.Password!);

            if (createdUserResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, BaseTypeRole.User.ToString());
            }

            return createdUserResult;
        }

        public async Task<IEnumerable<UserDto>?> GetAllAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = mapper.Map<UserDto>(user);

                var roles = string.Join(", ", await userManager.GetRolesAsync(user));
                userDto.Role = roles;

                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public async Task<UserDto?> TryGetByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return await GetUserDtoAsync(user);
        }

        

        public async Task<UserDto?> TryGetByLoginAsync(string login)
        {
            var user = await userManager.FindByNameAsync(login);

            return await GetUserDtoAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserDto updatedUser)
        {
            User? user = await userManager.FindByIdAsync(updatedUser.Id);

            if (user is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Пользователь не найден!"
                });
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber;

            return await userManager.UpdateAsync(user);
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

        private async Task<UserDto?> GetUserDtoAsync(User? user)
        {
            if (user == null) return null;

            var userDto = mapper.Map<UserDto>(user);

            var roles = await userManager.GetRolesAsync(user);

            userDto.Role = string.Join(", ", roles);

            return userDto;
        }
    }
}
