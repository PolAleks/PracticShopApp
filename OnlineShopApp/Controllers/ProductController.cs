using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class ProductController(ProductsRepository productsRepository) : Controller
    {
        private readonly ProductsRepository _productsRepository = productsRepository;

        public IActionResult Index(int id)
        {
            var product = _productsRepository.TryGetById(id);
            
            return View(product);
        }

        public IActionResult Add(string name, decimal cost, string description)
        {
            _productsRepository.Add(name, cost, description);

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
