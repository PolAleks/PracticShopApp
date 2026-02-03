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
    }
}
