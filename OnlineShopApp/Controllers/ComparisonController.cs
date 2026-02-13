using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class ComparisonController : Controller
    {
        private readonly IComparisonRepository _comparisonRepository;
        private readonly IProductsRepository _productsRepository;

        public ComparisonController(IComparisonRepository comparisonRepository, IProductsRepository productsRepository)
        {
            _comparisonRepository = comparisonRepository;
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var comparison = _comparisonRepository.TryGetByUserId(Constans.UserId);

            return View(comparison);
        }

        public IActionResult Add(int productId)
        {
            var product = _productsRepository.TryGetById(productId);
            if (product is not null)
            {
                _comparisonRepository.Add(product, Constans.UserId);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int productId)
        {
            _comparisonRepository.Delete(productId, Constans.UserId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _comparisonRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
