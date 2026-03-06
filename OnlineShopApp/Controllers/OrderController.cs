using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopApp.Helpers.Mapping;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Controllers
{
    [Authorize]
    public class OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository) : Controller
    {
        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);

            var order = new OrderViewModel()
            {
                Items = cart?.Items.ToViewModels().ToList() ?? []
            };

            return View(order);
        }

        [HttpPost]
        public IActionResult Buy(OrderViewModel order)
        {
            var cart = cartsRepository.TryGetByUserId(Constans.UserId);

            if (cart == null)
            {
                return View(nameof(Index), order);
            }

            order.UserId = Constans.UserId;
            order.Items = cart.Items.ToViewModels().ToList();

            if (!ModelState.IsValid)
            {
                return View(nameof(Index), order);
            }

            var orderDb = new Order()
            {
                Id = order.Id,
                UserId = order.UserId,
                Items = cart.Items,
                DeliveryUser = order.DeliveryUser.ToDbModel(),
                CreationDateTime = order.CreationDateTime,
                Status = (OrderStatus)order.Status,
            };

            ordersRepository.Add(orderDb);

            cartsRepository.Clear(Constans.UserId);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
