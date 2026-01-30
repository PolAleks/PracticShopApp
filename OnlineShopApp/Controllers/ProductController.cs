using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            var product = ProductsRepository.TryGetById(id);
            if (product is not null)
            {
                return $"{product}{product.Description}"; 
            }
            return $"Товар с идентификатором: {id} отсутствует!";
        }

        public IActionResult Add(string name, decimal cost, string description)
        {
            ProductsRepository.Add(name, cost, description);

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
