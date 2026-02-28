using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopApp.Helpers;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Controllers
{
    public class OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository) : Controller
    {
        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);

            var order = new Order()
            {
                Items = cart?.Items?.ToViewModels().ToList() ?? []
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);
            
            if (cart is null) 
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));

            order.UserId = Constans.UserId;
            order.Items = cart!.Items!.ToViewModels().ToList();

            if (!ModelState.IsValid)
            {
                return View(nameof(Index), order);
            }

            ordersRepository.Add(order);

            cartsRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
