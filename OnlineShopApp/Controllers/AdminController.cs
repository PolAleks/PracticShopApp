using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Controllers
{
    public class AdminController(IProductsRepository productsRepository, IOrdersRepository ordersRepository, IRolesRepository rolesRepository) : Controller
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IOrdersRepository _ordersRepository = ordersRepository;
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        #region Orders
        
        public IActionResult Orders()
        {
            var orders = _ordersRepository.GetAll();

            return View(orders);
        }

        public IActionResult DetailOrder(Guid id)
        {
            var order = _ordersRepository.TryGetById(id);

            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(Guid id, OrderStatus status)
        {
            _ordersRepository.UpdateStatus(id, status);

            return RedirectToAction(nameof(Orders));
        }

        #endregion

        #region Users

        public IActionResult Users()
        {
            return View();
        }

        #endregion

        #region Roles

        public IActionResult Roles()
        {
            var roles = _rolesRepository.GetAll();

            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            var existingName = _rolesRepository.TryGetByName(role.Name);

            if (existingName is not null)
            {
                ModelState.AddModelError("", "Такая роль уже существует!");
            }

            if (!ModelState.IsValid)
            {
                return View(role);
            }
            
            _rolesRepository.Add(role);

            return RedirectToAction(nameof(Roles));
        }

        public IActionResult DeleteRole(Guid roleId)
        {
            _rolesRepository.Delete(roleId);

            return RedirectToAction(nameof(Roles));
        }

        #endregion

        #region Products

        public IActionResult Products()
        {
            var products = _productsRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _productsRepository.Add(product);

            return RedirectToAction(nameof(Products));
        }

        public IActionResult DeleteProduct(int id)
        {
            _productsRepository.Delete(id);

            return RedirectToAction(nameof(Products));
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var product = _productsRepository.TryGetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _productsRepository.Update(product);

            return RedirectToAction(nameof(Products));
        }

        #endregion
    }
}
