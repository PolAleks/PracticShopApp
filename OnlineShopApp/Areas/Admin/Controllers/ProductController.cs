using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IProductsRepository productsRepository) : Controller
    {
        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productsRepository.Add(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            productsRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = productsRepository.TryGetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productsRepository.Update(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
