using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Infrastructure.Exceptions;
using OnlineShop.Web.ViewModels;

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
                var product = await productService.GetProductWithImagesByIdAsync(id);

                var productViewModel = new UpdateProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ExistingImages = product.ProductImages.Select(img => new ProductImageViewModel
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath,
                        IsMain = img.IsMain
                    }).ToList()
                };

                return View(productViewModel);
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
