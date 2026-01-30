using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class HomeController : Controller
    {      
        public IActionResult Index()
        {
            var products = ProductsRepository.GetAll();

            return View(products);
        }
    }
}
