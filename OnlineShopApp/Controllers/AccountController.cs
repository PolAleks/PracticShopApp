using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DAL.Entities;
using OnlineShop.Db.Models;
using OnlineShop.Web.ViewModels;
using OnlineShop.Web.ViewModels;

namespace OnlineShopApp.Controllers
{
    public class AccountController(UserManager<User> userManager,
                                   SignInManager<User> signInManager) : Controller
    {
        #region Authorization

        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(AuthorizationViewModel authorization, string? returnUrl)
        {
            if (authorization.Login == authorization.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            if (!ModelState.IsValid)
            {
                return View(authorization);
            }

            var result = await signInManager.PasswordSignInAsync(userName: authorization.Login, 
                                                                 password: authorization.Password, 
                                                                 isPersistent: authorization.IsRememberMe, 
                                                                 lockoutOnFailure: false);

            if (result.Succeeded) 
            {
                if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин или пароль!");
                return View(authorization);
            }
        }

        #endregion


        #region Registration
        public IActionResult Registration(string? returnUrl)
        {
            ViewData.Add(nameof(returnUrl), returnUrl);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel registration, string? ReturnUrl)
        {
            if (registration.Login == registration.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            User user = new()
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
                await userManager.AddToRoleAsync(user, BaseTypeRole.User.ToString());
                await signInManager.SignInAsync(user, isPersistent: false);

                if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }

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

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
