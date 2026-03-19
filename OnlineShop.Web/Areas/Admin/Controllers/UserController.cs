using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.DTO.User;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService, 
                                IRoleService roleService,
                                IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllAsync();

            var model = mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await userService.TryGetByIdAsync(id);

            var model = mapper.Map<UserViewModel>(user);

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserRegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var existingUser = await userService.TryGetByLoginAsync(registerViewModel.UserName);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Такой пользователь уже существует!");
                return View(registerViewModel);
            }

            var userDto = mapper.Map<UserRegisterDto>(registerViewModel);

            await userService.CreateUserAsync(userDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await userService.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(string id)
        {
            var userDto = await userService.TryGetByIdAsync(id);

            if (userDto == null) return NotFound();

            var editUserModel = mapper.Map<EditUserViewModel>(userDto);

            return View(editUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(EditUserViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            var updateUserDto = mapper.Map<UpdateUserDto>(editViewModel);

            await userService.UpdateUserAsync(updateUserDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(string id)
        {
            var existingUser = await userService.TryGetByIdAsync(id);

            if (existingUser == null) return NotFound();

            var roles = await roleService.GetAllRolesAsync();

            var changeRole = new ChangeRoleViewModel()
            {
                Id = existingUser.Id,
                Role = existingUser.Role,
                Roles = roles.Select(role => new SelectListItem 
                { 
                    Text = role.Name.ToString(), 
                    Value = role.Name 
                }).ToList()
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

            var role = await roleService.GetRoleByNameAsync(changeRole.Role);

            if (role is not null)
            {
                var roleDto = mapper.Map<ChangeUserRoleDto>(changeRole);

                await userService.ChangeRoleAsync(roleDto);
            }

            return RedirectToAction(nameof(Detail), new { changeRole.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var existingUser = await userService.TryGetByIdAsync(id);

            if(existingUser == null) return NotFound();

            var changePasswordViewModel = new ChangeUserPasswordViewModel() 
            { 
                Id = existingUser.Id 
            };

            return View(changePasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordViewModel);
            }

            var changePasswordDto = mapper.Map<ChangeUserPasswordDto>(changePasswordViewModel);

            var result = await userService.ChangePasswordAsync(changePasswordDto);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Detail), new { changePasswordViewModel.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(changePasswordViewModel);
        }
    }
}
