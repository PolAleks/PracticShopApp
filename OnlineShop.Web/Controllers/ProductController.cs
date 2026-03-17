using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class ProductController(IProductService productService, IMapper mapper) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);

            var productViewModel = _mapper.Map<ProductViewModel>(productDto);
            
            return View(productViewModel);
        }
    }
}
