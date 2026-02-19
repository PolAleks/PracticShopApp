using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController(IRolesRepository rolesRepository) : Controller
    {
        public IActionResult Index()
        {
            var roles = rolesRepository.GetAll();

            return View(roles);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
            var existingName = rolesRepository.TryGetByName(role.Name);

            if (existingName is not null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }

            if (!ModelState.IsValid)
            {
                return View(role);
            }

            rolesRepository.Add(role);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid roleId)
        {
            rolesRepository.Delete(roleId);

            return RedirectToAction(nameof(Index));
        }
    }
}
