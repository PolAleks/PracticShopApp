using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Web.Helpers.Mapping;

namespace OnlineShop.Web.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartsRepository cartsRepository) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);
            var countProduct = cart?.ToViewModel().Quantity ?? 0;

            return View(nameof(Cart), countProduct);
        }
    }
}
