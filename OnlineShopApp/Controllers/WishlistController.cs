using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class WishlistController(IWishlistsRepository wishlistsRepository, IProductsRepository productsRepository) : Controller
    {
        private readonly IWishlistsRepository _wishlistsRepository = wishlistsRepository;
        private readonly IProductsRepository _productsRepository = productsRepository;
        public IActionResult Index()
        {
            var wishlist = _wishlistsRepository.TryGetWishlistByUserId(Constans.UserId);

            return View(wishlist);
        }

        public IActionResult Add(int productId)
        {
            var existingProduct = _productsRepository.TryGetById(productId);
            if (existingProduct is not null)
            {
                _wishlistsRepository.Add(existingProduct, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int productId)
        {
            var existingProduct = _productsRepository.TryGetById(productId);
            if (existingProduct is not null)
            {
                _wishlistsRepository.Remove(existingProduct, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _wishlistsRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
