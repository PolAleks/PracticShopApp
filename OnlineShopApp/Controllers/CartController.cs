using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class CartController : Controller
    {
        private readonly CartsRepository _cartsRepository;
        private readonly ProductsRepository _productsRepository;

        public CartController(CartsRepository cartsRepository, ProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

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
