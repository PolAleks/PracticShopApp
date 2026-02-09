using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
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
            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            return View();
        }
    }
}
