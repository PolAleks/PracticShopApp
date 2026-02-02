using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var card = CartRepository.TryGetbyUserId(Constans.UserId);
            return View(card);
        }
    }
}
