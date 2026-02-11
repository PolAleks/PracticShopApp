using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

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
        #endregion
    }
}
