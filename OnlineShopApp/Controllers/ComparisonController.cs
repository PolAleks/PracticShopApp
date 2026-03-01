using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopApp.Helpers;

namespace OnlineShopApp.Controllers
{
    public class ComparisonController(IComparisonRepository comparisonRepository, IProductsRepository productsRepository) : Controller
    {
        public IActionResult Index()
        {
            var comparison = comparisonRepository.TryGetByUserId(Constans.UserId);

            return View(comparison?.ToViewModel());
        }

        public IActionResult Add(int productId)
        {
            var product = productsRepository.TryGetById(productId);

            if (product is not null)
            {
                comparisonRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int productId)
        {
            comparisonRepository.Delete(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            comparisonRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
