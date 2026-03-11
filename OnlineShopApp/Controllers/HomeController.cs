using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShopApp.Helpers.Mapping;

namespace OnlineShopApp.Controllers
{
    public class HomeController(IProductsRepository productsRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();

            return View(products.ToViewModels());
        }

        [HttpGet]
        public IActionResult Search(string? query)
        {
            if (query is null)
            {
                return View();
            }

            var products = _productsRepository.Search(query!);

            return View(products.ToViewModels());
        }
    }
}
