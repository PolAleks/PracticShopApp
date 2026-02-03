using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class CartController : Controller
    {
        private readonly CartsRepository _cartsRepository;

        public CartController(CartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var card = _cartsRepository.TryGetbyUserId(Constans.UserId);
            return View(card);
        }

        public IActionResult Add(int productId)
        {
            var product = ProductsRepository.TryGetById(productId);
            if (product is not null)
            {
                _cartsRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
