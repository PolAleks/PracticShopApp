using Microsoft.AspNetCore.Identity;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>?> GetAllAsync();
        Task<UserViewModel?> TryGetByIdAsync(Guid userId);
        Task<UserViewModel?> TryGetByLoginAsync(string login);
        Task<IdentityResult> CreateUserAsync(UserViewModel user);
        Task<IdentityResult> UpdateUserAsync(EditUserViewModel user);
        Task<IdentityResult> DeleteUserAsync(Guid userId);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel changePassword);
        Task<IdentityResult> ChangeRoleAsync(ChangeRoleViewModel changeRole);        
    }
}
