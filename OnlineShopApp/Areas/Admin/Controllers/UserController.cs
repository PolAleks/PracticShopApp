using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService, 
                                IRolesRepository rolesRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllAsync();
            
            return View(users);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var user = await userService.TryGetByIdAsync(id);

            return View(user);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            
            var existingUser = await userService.TryGetByLoginAsync(newUser.Login!);

            if (existingUser is not null)
            {
                ModelState.AddModelError("", "Такой пользователь уже существует!");
                return View(newUser);
            }

            await userService.CreateUserAsync(newUser);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid userId)
        {
             await userService.DeleteUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(Guid userId)
        {
            var user = await userService.TryGetByIdAsync(userId);
            
            if (user is null) return NotFound();

            EditUserViewModel editUser = new()
            {
                Id = user.Id.ToString(),
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
            };

            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(EditUserViewModel updateUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updateUser);
            }

            await userService.UpdateUserAsync(updateUser);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(Guid userId)
        {
            var existingUser = await userService.TryGetByIdAsync(userId);
                          
            if (existingUser is null) return NotFound();

            var changeRole = new ChangeRoleViewModel()
            {
                Id = existingUser.Id,
                Role = existingUser.Role,
                Roles = rolesRepository.GetAll()
                    .Select(role => new SelectListItem { Text = role.Name.ToString(), Value = role.Name })
                    .ToList()
            };

            return View(changeRole);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(ChangeRoleViewModel changeRole)
        {
            if (!ModelState.IsValid)
            {
                return View(changeRole);
            }

            var role = rolesRepository.TryGetByName(changeRole.Role);
            
            if (role is not null)
            {
                await userService.ChangeRoleAsync(changeRole);
            }

            return RedirectToAction(nameof(Detail), new { changeRole.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(Guid userId)
        {
            var existingUser = await userService.TryGetByIdAsync(userId);

            ChangePasswordViewModel changePassword = new() { Id = existingUser.Id };

            return View(changePassword);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }
            
            IdentityResult result = await userService.ChangePasswordAsync(changePassword);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Detail), new { changePassword.Id });
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(changePassword);
        }
    }
}
