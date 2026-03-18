using Microsoft.AspNetCore.Identity;
using OnlineShop.Core.DTO;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(string id);
        Task<RoleDto> GetRoleByNameAsync(string name);
        Task<IdentityResult> CreateRoleAsync(string newRole);
        Task RemoveRoleByIdAsync(string id);

    }
}
