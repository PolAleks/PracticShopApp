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

        public IActionResult Add(int productId)
        {
            var product = ProductsRepository.TryGetById(productId);
            if (product is not null)
            {
                CartRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
