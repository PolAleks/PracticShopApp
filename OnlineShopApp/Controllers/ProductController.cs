using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id)
        {
            var product = ProductsRepository.TryGetById(id);
            
            return View(product);
        }

        public IActionResult Add(string name, decimal cost, string description)
        {
            ProductsRepository.Add(name, cost, description);

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
