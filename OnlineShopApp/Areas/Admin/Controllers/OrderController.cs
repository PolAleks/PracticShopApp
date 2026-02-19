using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController(IOrdersRepository ordersRepository) : Controller
    {     
        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();

            return View(orders);
        }

        public IActionResult Detail(Guid id)
        {
            var order = ordersRepository.TryGetById(id);

            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid id, OrderStatus status)
        {
            ordersRepository.UpdateStatus(id, status);

            return RedirectToAction(nameof(Index));
        }
    }
}
