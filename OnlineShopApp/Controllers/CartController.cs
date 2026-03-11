using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Web.Helpers.Mapping;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository) : Controller
    {

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);

            return View(cart?.ToViewModel());
        }

        public IActionResult Add(int productId)
        {
            var product = productsRepository.TryGetById(productId);
            if (product is not null)
            {
                cartsRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Subtract(int productId)
        {
            cartsRepository.Subtract(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            cartsRepository.Clear(Constans.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
