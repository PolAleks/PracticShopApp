using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController(IUsersRepository usersRepository, IRolesRepository rolesRepository) : Controller
    {
        public IActionResult Index()
        {
            var users = usersRepository.GetAll();

            return View(users);
        }

        public IActionResult Detail(Guid id)
        {
            var user = usersRepository.TryGetById(id);

            return View(user);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(User newUser)
        {
            var existingUser = usersRepository.TryGetByLogin(newUser.Login);

            if (existingUser is not null)
            {
                ModelState.AddModelError("", "Такой пользователь уже существует!");
            }

            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            usersRepository.Add(newUser);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            usersRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var user = usersRepository.TryGetById(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User updateUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updateUser);
            }

            usersRepository.Update(updateUser);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangeRole(Guid id)
        {
            var existingUser = usersRepository.TryGetById(id);

            var changeRole = new ChangeRole()
            {
                Login = existingUser?.Login,
                Role = existingUser?.Role?.ToString(),
                Roles = rolesRepository.GetAll()
                    .Select(role => new SelectListItem { Text = role.Name.ToString(), Value = role.Name })
                    .ToList()
            };

            return View(changeRole);
        }

        [HttpPost]
        public IActionResult ChangeRole(ChangeRole changeRole)
        {
            if (!ModelState.IsValid)
            {
                return View(changeRole);
            }

            var login = changeRole.Login;
            var role = rolesRepository.TryGetByName(changeRole.Role);
            
            if (role is not null)
            {
                usersRepository.ChangeRole(login, role);
            }

            return RedirectToAction(nameof(Detail), new { usersRepository.TryGetByLogin(login)?.Id });
        }

        [HttpGet]
        public IActionResult ChangePassword(Guid id)
        {
            var existingUser = usersRepository.TryGetById(id);

            ChangePassword changePassword = new() { Login = existingUser?.Login };

            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            var existingUser = usersRepository.TryGetByLogin(changePassword.Login);

            if (existingUser?.Password == changePassword.Password)
            {
                ModelState.AddModelError("", "Нельзя использовать старый пароль!");
            }

            if (changePassword.Login == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }

            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }

            usersRepository.ChangePassword(changePassword.Login, changePassword.Password);

            return RedirectToAction(nameof(Detail), new { usersRepository.TryGetByLogin(changePassword.Login)?.Id });
        }
    }
}
