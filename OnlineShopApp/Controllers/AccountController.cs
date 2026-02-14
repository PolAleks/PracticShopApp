using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
using System.Net;
using Authorization = OnlineShopApp.Models.Authorization;

namespace OnlineShopApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Authorization()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Authorization(Authorization authorization)
        {
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

            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
