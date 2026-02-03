using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class CartController(CartsRepository cartsRepository, ProductsRepository productsRepository) : Controller
    {
        private readonly CartsRepository _cartsRepository = cartsRepository;
        private readonly ProductsRepository _productsRepository = productsRepository;

        public IActionResult Index()
        {
            var card = _cartsRepository.TryGetbyUserId(Constans.UserId);
            return View(card);
        }

        public IActionResult Add(int productId)
        {
            var product = _productsRepository.TryGetById(productId);
            if (product is not null)
            {
                _cartsRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
