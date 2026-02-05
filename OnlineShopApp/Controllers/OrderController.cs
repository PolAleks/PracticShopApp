using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;

namespace OnlineShopApp.Controllers
{
    public class OrderController(ICartsRepository cartsRepository) : Controller
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;
        public IActionResult Index()
        {
            var cart = _cartsRepository.TryGetByUserId(Constans.UserId);

            return View(cart);
        }
    }
}
