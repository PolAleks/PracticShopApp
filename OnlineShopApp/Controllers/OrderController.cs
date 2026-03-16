using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class OrderController(ICartService cartService,
                                 IOrderService orderService,
                                 IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cartDto = await cartService.GetCartAsync(Constans.UserId);

            var orderItems = mapper.Map<List<ItemViewModel>>(cartDto.Items);

            var orderViewModel = new OrderViewModel
            {
                Items = orderItems
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), orderViewModel);
            }

            CreateOrderDto orderDto = new()
            {
                UserId = Constans.UserId,
                DeliveryUser = mapper.Map<DeliveryUserDto>(orderViewModel.DeliveryUser)
            };

            await orderService.CreateOrderAsync(orderDto);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
