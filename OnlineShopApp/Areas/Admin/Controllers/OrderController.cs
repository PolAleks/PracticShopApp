using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Web.ViewModels;

namespace OnlineShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController(IOrderService orderService,
                                 IMapper mapper) : Controller
    {     
        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetAllOrderAsync();

            var ordersModel = mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return View(ordersModel);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var order = await orderService.GetOrderByIdAsync(id);

            var viewModel = mapper.Map<OrderViewModel>(order);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatusViewModel status)
        {
            await orderService.UpdateStatusAsync(id, (OrderStatus)status);

            return RedirectToAction(nameof(Index));
        }
    }
}
