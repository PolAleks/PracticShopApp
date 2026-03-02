using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopApp.Helpers;
using OnlineShopApp.Helpers.Mapping;

namespace OnlineShopApp.Controllers
{
    public class FavoriteController(IFavoritesRepository favoritesRepository, IProductsRepository productsRepository) : Controller
    {

        public IActionResult Index()
        {
            var favorite = favoritesRepository.TryGetByUserId(Constans.UserId);

            return View(favorite.ToViewModel());
        }

        public IActionResult Add(int productId)
        {
            var product = productsRepository.TryGetById(productId);

            if (product is not null)
            {
                favoritesRepository.Add(product, Constans.UserId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
            }
        }

        public IActionResult Delete(int productId)
        {
            favoritesRepository.Delete(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            favoritesRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
