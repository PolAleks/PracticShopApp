using Microsoft.AspNetCore.Identity;
using OnlineShop.BLL.DTO;

namespace OnlineShopApp.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>?> GetAllAsync();
        Task<UserDto?> TryGetByIdAsync(Guid userId);
        Task<UserDto?> TryGetByLoginAsync(string login);
        Task<IdentityResult> CreateUserAsync(UserRegisterDto user);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto user);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<IdentityResult> ChangePasswordAsync(string id, string password);
        Task<IdentityResult> ChangeRoleAsync(string id, string role);        
    }
}
