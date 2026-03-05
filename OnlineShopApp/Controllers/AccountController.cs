using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models.IdentityEntities;
using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;
using System.Threading.Tasks;
using Authorization = OnlineShopApp.Models.Authorization;

namespace OnlineShopApp.Controllers
{
    public class AccountController(IUsersRepository usersRepository, UserManager<ApplicationUser> userManager) : Controller
    {
        #region Authorization

        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(Authorization authorization)
        {
            if (authorization.Login == authorization.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            var user = usersRepository.TryGetByLogin(authorization.Login);

            if (user is null)
            {
                ModelState.AddModelError("", "Такого пользователя не существует!\r\nПройдите регистрацию!");
            }
            else if (user.Password != authorization.Password)
            {
                ModelState.AddModelError("", "Неправильный пароль!");
            }

            if (!ModelState.IsValid)
            {
                return View(authorization);
            }

            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }
        #endregion


        #region Registration
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Registration registration)
        {
            if (registration.Login == registration.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            //var existingUser = usersRepository.TryGetByLogin(registration.Login);

            //if (existingUser is not null)
            //{
            //    ModelState.AddModelError("", "Пользователь с таким именем уже зарегистрирован!\r\nНеобходимо зарегистрироваться под другим логином");
            //}

            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            ApplicationUser user = new()
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Login,
                UserName = registration.Login,
                PhoneNumber = registration.Phone
            };

            IdentityResult result = await userManager.CreateAsync(user, registration.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registration);
            }            
        }
        #endregion
    }
}
