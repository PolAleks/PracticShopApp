using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models.ViewModels.Product;

namespace OnlineShopApp.Controllers
{
    public class ProductController(IProductsRepository productsRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index(int id)
        {
            var product = _productsRepository.TryGetById(id);
            
            return View(product);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, decimal cost, string description)
        {
            _productsRepository.Add(name, cost, description);

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var updatedProduct = _productsRepository.TryGetById(id);

            EditProductViewModel editProductView = new()
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Cost = updatedProduct.Cost,
                Description = updatedProduct.Description
            };

            return View(editProductView);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel editProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(editProduct);
            }

            _productsRepository.Update(editProduct);

            return RedirectToAction(nameof(Edit));
        }
    }
}
