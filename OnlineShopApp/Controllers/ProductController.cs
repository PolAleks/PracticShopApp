using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class ProductController(IProductsRepository productsRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index(int id)
        {
            var product = _productsRepository.TryGetById(id);
            
            return View(product);
        }
    }
}
