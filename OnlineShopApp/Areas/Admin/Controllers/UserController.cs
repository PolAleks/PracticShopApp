using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IUsersRepository usersRepository) : Controller
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
    }
}
