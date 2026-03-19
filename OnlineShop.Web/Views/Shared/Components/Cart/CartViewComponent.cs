using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;

namespace OnlineShop.Web.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartService cartService,
                                   ICurrentUserService currentUser) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await cartService.GetCartAsync(currentUser.UserName);

            var countProduct = cart.Items.Sum(i => i.Quantity);

            return View(nameof(Cart), countProduct);
        }
    }
}
