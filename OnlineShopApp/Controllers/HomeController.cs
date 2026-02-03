using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class HomeController : Controller
    {      
        private readonly ProductsRepository _productsRepository;

        public HomeController(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();

            return View(products);
        }
    }
}
