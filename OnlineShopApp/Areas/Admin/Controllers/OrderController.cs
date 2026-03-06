using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopApp.Helpers.Mapping;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController(IOrdersRepository ordersRepository) : Controller
    {     
        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();

            return View(orders.ToViewModels());
        }

        public IActionResult Detail(Guid id)
        {
            var order = ordersRepository.TryGetById(id);

            return View(order.ToViewModel());
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid id, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(id, (OrderStatus)status);

            return RedirectToAction(nameof(Index));
        }
    }
}
