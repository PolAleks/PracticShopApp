using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartsRepository cartsRepository) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);
            var countProduct = cart?.Quantity ?? 0;

            return View(nameof(Cart), countProduct);
        }
        
    }
}
