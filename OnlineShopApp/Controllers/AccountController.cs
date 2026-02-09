using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Registration()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
