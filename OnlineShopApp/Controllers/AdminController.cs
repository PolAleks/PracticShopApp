using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Controllers
{
    public class AdminController(IProductsRepository productsRepository) : Controller
    {
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }

        #region Products

        public IActionResult Products()
        {
            var products = _productsRepository.GetAll();
            return View(products);
        }

        public IActionResult DeleteProduct(int id) 
        {
            _productsRepository.Delete(id);

            return RedirectToAction(nameof(Products));
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var product = _productsRepository.TryGetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _productsRepository.Update(product);

            return RedirectToAction(nameof(Products));
        }

        #endregion
    }
}
