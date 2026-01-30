using Microsoft.AspNetCore.Mvc;

namespace OnlineShopApp.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
