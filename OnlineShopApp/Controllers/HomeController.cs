using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class HomeController(IProductsRepository productsRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();

            return View(products);
        }

        [HttpGet]
        public IActionResult Search(string? query)
        {
            if (query is not null)
            {
                var products = _productsRepository.Search(query);
                return View(products);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
