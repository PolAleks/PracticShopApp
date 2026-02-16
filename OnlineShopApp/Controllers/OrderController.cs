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

            var order = new Order()
            {
                Items = cart?.Items ?? []
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            var cart = _cartsRepository.TryGetByUserId(Constans.UserId);
            
            if (cart is null) 
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));

            order.UserId = Constans.UserId;
            order.Items = cart!.Items!;

            if (!ModelState.IsValid)
            {
                return View(nameof(Index), order);
            }

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
