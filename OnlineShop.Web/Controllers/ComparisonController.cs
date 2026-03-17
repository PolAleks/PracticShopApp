using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class ComparisonController(IComparisonService comparisonService,
                                      IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var comparisonDto = await comparisonService.GetByUserIdAsync(Constans.UserId);

            var comparisonViewModel = mapper.Map<ComparisonViewModel>(comparisonDto);

            return View(comparisonViewModel);
        }

        public async Task<IActionResult> Add(int productId)
        {
            await comparisonService.AddToComparisonAsync(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int productId)
        {
            await comparisonService.RemoveFromComparisonAsync(productId, Constans.UserId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await comparisonService.ClearComparisonAsync(Constans.UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
