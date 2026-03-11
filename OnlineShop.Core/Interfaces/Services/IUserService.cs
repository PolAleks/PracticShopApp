using Microsoft.AspNetCore.Identity;
using OnlineShop.Core.DTO.User;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>?> GetAllAsync();
        Task<UserDto?> TryGetByIdAsync(string id);
        Task<UserDto?> TryGetByLoginAsync(string login);
        Task<IdentityResult> CreateUserAsync(RegisterUserDto user);
        Task<IdentityResult> UpdateUserAsync(UpdateUserDto user);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<IdentityResult> ChangePasswordAsync(ChangeUserPasswordDto passwordDto);
        Task<IdentityResult> ChangeRoleAsync(ChangeUserRoleDto roleDto);        
    }
}
