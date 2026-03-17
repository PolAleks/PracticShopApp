using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Data
{
    public class IdentityInit
    {
        public static async Task IntitFirstData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminLogin = "admin@localhost.com";
            string adminPassword = "qwerty1";

            string[] roles = Enum.GetNames(typeof(BaseTypeRole));

            foreach (var nameRole in roles)
            {
                if (await roleManager.FindByNameAsync(nameRole) == null)
                {
                    IdentityRole role = new() { Name = nameRole };
                    
                    await roleManager.CreateAsync(role);
                }
            }

            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                User user = new()
                {
                    Email = adminLogin,
                    UserName = adminLogin,
                };
                
                IdentityResult result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, BaseTypeRole.Admin.ToString());
                }
            }
        }
    }
}
