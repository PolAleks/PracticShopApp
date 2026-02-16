using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository) : Controller
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index()
        {
            var cart = _cartsRepository.TryGetByUserId(Constans.UserId);

            return View(cart);
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

        public IActionResult Subtract(int productId)
        {
            _cartsRepository.Subtract(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartsRepository.Clear(Constans.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
