using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShop.Db.Models.IdentityEntities;

namespace OnlineShop.Db
{
    public class IdentityInit
    {
        public static async Task IntitFirstData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            string adminLogin = "admin@localhost.com";
            string adminPassword = "qwerty1";

            var roles = Enum.GetNames(typeof(BaseTypeRole));

            foreach (var nameRole in roles)
            {
                if (await roleManager.FindByNameAsync(nameRole) == null)
                {
                    ApplicationRole role = new() { Name = nameRole };
                    
                    await roleManager.CreateAsync(role);
                }
            }

            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                ApplicationUser user = new()
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
