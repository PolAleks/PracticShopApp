using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Web.ViewModels;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Core.DTO;
using OnlineShop.Infrastructure.Exceptions;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController(IProductService productService,
                                   IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var productsDto = await productService.GetAllProductsAsync();

            return View(mapper.Map<IEnumerable<ProductViewModel>>(productsDto));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var productDto = mapper.Map<CreateProductDto>(viewModel);

            _ = await productService.CreateProductAsync(productDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            _ = await productService.DeleteProductByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var productDto = await productService.GetProductByIdAsync(id);

                return View(mapper.Map<UpdateProductViewModel>(productDto));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var productDto = mapper.Map<UpdateProductDto>(productModel);

            try
            {
                _ = await productService.UpdateProductAsync(productDto);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
