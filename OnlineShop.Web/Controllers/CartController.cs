using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    
    public class CartController(ICartService cartService, 
                                IMapper mapper,
                                ICurrentUserService currentUser) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var cartDto = await cartService.GetCartAsync(currentUser.UserName);

            var cartModel = mapper.Map<CartViewModel>(cartDto);

            return View(cartModel);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            await cartService.AddToCartAsync(currentUser.UserName, productId); ;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Increase(int productId)
        {
            await cartService.IncreaseQuantityAsync(currentUser.UserName, productId);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Decrease(int productId)
        {
            await cartService.DecreaseQuantityAsync(currentUser.UserName, productId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await cartService.ClearCartAsync(currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }
    }
}
