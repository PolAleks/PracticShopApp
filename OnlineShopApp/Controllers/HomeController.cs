using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class HomeController(IProductService productService ,IMapper mapper) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var productsDto = await _productService.GetAllProductsAsync();

            var productsViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(productsDto);

            return View(productsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? query)
        {
            if (query is null)
            {
                return View();
            }

            var productsDto = await _productService.SearchProductsAsync(query);

            var productsViewModel = _mapper.Map<ProductViewModel>(productsDto);

            return View(productsViewModel);
        }
    }
}
