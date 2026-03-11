using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
