using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;

namespace OnlineShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Index()
        {
            List<Product> products = InitialProducts();

            var result = new StringBuilder();
            foreach (var product in products)
            {
                result.AppendLine($"ID: {product.Id} - Name: {product.Name} - Cost: {product.Cost}");
            }

            return result.ToString();
        }

        private static List<Product> InitialProducts()
        {
            return new List<Product>()
            {
                new(){Id = 1, Name = "product 1", Cost = 90},
                new(){Id = 2, Name = "product 2", Cost = 100},
                new(){Id = 3, Name = "product 3", Cost = 85}
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
