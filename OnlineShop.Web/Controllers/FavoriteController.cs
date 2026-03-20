using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class FavoriteController(IFavoriteService favoriteService,
                                    IMapper mapper,
                                    ICurrentUserService currentUser) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var favoriteDto = await favoriteService.GetByUserIdAsync(currentUser.UserName);

            var favoriteViewModel = mapper.Map<FavoriteViewModel>(favoriteDto);
            
            return View(favoriteViewModel);
        }

        public async Task<IActionResult> Add(int productId)
        {
            await favoriteService.AddToFavoriteAsync(productId, currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int productId)
        {
            await favoriteService.RemoveFromFavoriteAsync(productId, currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await favoriteService.ClearFavoriteAsync(currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }
    }
}
