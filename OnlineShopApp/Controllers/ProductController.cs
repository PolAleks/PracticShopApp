using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShopApp.Helpers.Mapping;

namespace OnlineShopApp.Controllers
{
    public class ProductController(IProductsRepository productsRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index(int id)
        {
            var product = _productsRepository.TryGetById(id);
            
            return View(product?.ToViewModel());
        }
    }
}
