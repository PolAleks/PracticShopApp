using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Infrastructure.Exceptions;

namespace OnlineShop.Infrastructure.Services
{
    public class RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper) : IRoleService
    {
        public async Task<IdentityResult> CreateRoleAsync(string newRole)
        {
            var existRole = await roleManager.RoleExistsAsync(newRole);
            if (!existRole)
            {
                IdentityRole role = new() { Name = newRole };
                return await roleManager.CreateAsync(role);
            }
            return IdentityResult.Failed(new IdentityError() { Description = $"{newRole} уже существует!" });
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var rolesDto = await roleManager.Roles
                .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return rolesDto;
        }

        public async Task<RoleDto> GetRoleByIdAsync(string id)
        {
            var role = await GetRoleAsync(id) ?? 
                throw new NotFoundException($"Роль с идентификатором {id} не найдена!");

            var roleDto = mapper.Map<RoleDto>(role);

            return roleDto;
        }

        public async Task<RoleDto> GetRoleByNameAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name) ??
                throw new NotFoundException($"Роль {name} не найдена!");
            
            var roleDto = mapper.Map<RoleDto>(role);

            return roleDto;
        }

        public async Task RemoveRoleByIdAsync(string id)
        {
            var existRole = await GetRoleAsync(id);

            if(existRole != null)
            {
                _ = await roleManager.DeleteAsync(existRole);
            }
        }

        private async Task<IdentityRole?> GetRoleAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }
    }
}
