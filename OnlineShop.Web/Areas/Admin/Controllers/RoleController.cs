using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController(IRoleService roleService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var rolesDto = await roleService.GetAllRolesAsync();

            var roleViewModel = mapper.Map<IEnumerable<RoleViewModel>>(rolesDto);

            return View(roleViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            var result = await roleService.CreateRoleAsync(role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(role);
        }

        public async Task<IActionResult> Delete(string roleId)
        {
            await roleService.RemoveRoleByIdAsync(roleId);

            return RedirectToAction(nameof(Index));
        }
    }
}
