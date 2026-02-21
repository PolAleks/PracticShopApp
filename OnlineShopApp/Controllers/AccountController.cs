using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;
using Authorization = OnlineShopApp.Models.Authorization;

namespace OnlineShopApp.Controllers
{
    public class AccountController(IUsersRepository usersRepository) : Controller
    {
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(Authorization authorization)
        {
            if (authorization.Login == authorization.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            var user = usersRepository.TryGetByLogin(authorization.Login);

            if (user is null)
            {
                ModelState.AddModelError("", "Такого пользователя не существует!\r\nПройдите регистрацию!");
            }
            else if (user.Password != authorization.Password)
            {
                ModelState.AddModelError("", "Неправильный пароль!");
            }

            if (!ModelState.IsValid)
            {
                return View(authorization);
            }

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            if (registration.Login == registration.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            var existingUser = usersRepository.TryGetByLogin(registration.Login);

            if (existingUser is not null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже зарегистрирован!\r\nНеобходимо зарегистрироваться под другим логином");
            }

            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            User newUser = new()
            {
                Login = registration.Login,
                Password = registration.Password
            };

            usersRepository.Add(newUser);

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
