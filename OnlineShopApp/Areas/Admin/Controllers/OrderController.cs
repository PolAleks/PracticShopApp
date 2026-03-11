using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Domain.Entities;
using OnlineShop.Web.Helpers.Mapping;
using OnlineShop.Web.ViewModels;

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
