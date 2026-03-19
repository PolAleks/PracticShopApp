using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTO.User;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Controllers
{
    public class AccountController(IAuthService authService, IMapper mapper) : Controller
    {

        #region Authorization
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(UserLoginViewModel loginViewModel , string? returnUrl)
        {
            if (loginViewModel.Login == loginViewModel.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var loginDto = mapper.Map<UserLoginDto>(loginViewModel);

            var result = await authService.LoginAsync(loginDto);

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
                return View(loginViewModel);
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
        public async Task<IActionResult> Registration(UserRegisterViewModel registerViewModel, string? ReturnUrl)
        {
            if (registerViewModel.UserName == registerViewModel.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var userRegisterDto = mapper.Map<UserRegisterDto>(registerViewModel);

            var result = await authService.RegisterAsync(userRegisterDto);

            if (result.Succeeded)
            {
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
                return View(registerViewModel);
            }            
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
