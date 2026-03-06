using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopApp.Helpers.Mapping;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController(IProductsRepository productsRepository) : Controller
    {
        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products.ToViewModels());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productsRepository.Add(product.ToDbModel());

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            productsRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = productsRepository.TryGetById(id);
            return View(product?.ToViewModel());
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productsRepository.Update(product.ToDbModel());

            return RedirectToAction(nameof(Index));
        }
    }
}
