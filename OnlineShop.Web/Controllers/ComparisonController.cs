using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class ComparisonController(IComparisonService comparisonService,
                                      IMapper mapper,
                                      ICurrentUserService currentUser) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var comparisonDto = await comparisonService.GetByUserIdAsync(currentUser.UserName);

            var comparisonViewModel = mapper.Map<ComparisonViewModel>(comparisonDto);

            return View(comparisonViewModel);
        }

        public async Task<IActionResult> Add(int productId)
        {
            await comparisonService.AddToComparisonAsync(productId, currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int productId)
        {
            await comparisonService.RemoveFromComparisonAsync(productId, currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await comparisonService.ClearComparisonAsync(currentUser.UserName);

            return RedirectToAction(nameof(Index));
        }
    }
}
