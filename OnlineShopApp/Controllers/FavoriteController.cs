using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;
using System.Threading.Tasks;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class FavoriteController(IFavoriteService favoriteService, IMapper mapper) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var favoriteDto = await favoriteService.GetByUserIdAsync(Constans.UserId);

            var favoriteViewModel = mapper.Map<FavoriteViewModel>(favoriteDto);
            
            return View(favoriteViewModel);
        }

        public async Task<IActionResult> Add(int productId)
        {
            await favoriteService.AddToFavoriteAsync(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int productId)
        {
            await favoriteService.RemoveFromFavoriteAsync(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await favoriteService.ClearFavoriteAsync(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
