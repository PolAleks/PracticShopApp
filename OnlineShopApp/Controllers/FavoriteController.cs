using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class FavoriteController(IFavoritesRepository favoritesRepository, IProductsRepository productsRepository) : Controller
    {
        private readonly IFavoritesRepository _favoritesRepository = favoritesRepository;
        private readonly IProductsRepository _productsRepository = productsRepository;
        public IActionResult Index()
        {
            var favorite = _favoritesRepository.TryGetByUserId(Constans.UserId);

            return View(favorite);
        }

        public IActionResult Add(int productId)
        {
            var product = _productsRepository.TryGetById(productId);

            if (product is not null)
            {
                _favoritesRepository.Add(product, Constans.UserId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
            }
        }

        public IActionResult Delete(int productId)
        {
            _favoritesRepository.Delete(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _favoritesRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
