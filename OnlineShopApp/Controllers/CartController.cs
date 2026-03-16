using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class CartController(ICartService cartService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cartDto = await cartService.GetCartAsync(Constans.UserId);

            var cartModel = mapper.Map<CartViewModel>(cartDto);

            return View(cartModel);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            await cartService.AddToCartAsync(Constans.UserId, productId); ;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Increase(int productId)
        {
            await cartService.IncreaseQuantityAsync(Constans.UserId, productId);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Decrease(int productId)
        {
            await cartService.DecreaseQuantityAsync(Constans.UserId, productId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await cartService.ClearCartAsync(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
