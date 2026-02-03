using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class HomeController(ProductsRepository productsRepository) : Controller
    {      
        private readonly ProductsRepository _productsRepository = productsRepository;

        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();

            return View(products);
        }
    }
}
