using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Controllers
{
    public class OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository) : Controller
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;
        private readonly IOrdersRepository _ordersRepository = ordersRepository;
        public IActionResult Index()
        {
            var cart = _cartsRepository.TryGetByUserId(Constans.UserId);

            return View(cart);
        }

        public IActionResult Buy()
        {
            var cart = _cartsRepository.TryGetByUserId(Constans.UserId);
            
            if (cart is null) 
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));

            var order = new Order
            {
                UserId = Constans.UserId,
                Items = cart.Items
            };

            _ordersRepository.Add(order);

            _cartsRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
