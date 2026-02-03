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
            var card = _cartsRepository.TryGetByUserId(Constans.UserId);
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
