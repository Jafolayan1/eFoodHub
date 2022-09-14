using eFoodHub.Entities;
using eFoodHub.Services.Interfaces;
using eFoodHub.UI.Models;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                User user = new()
                {
                    Name = model.Name,
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber
                };
                bool result = _authService.CreateUser(user, model.Password);
                if (result)
                {
                    return RedirectToAction(nameof(Login));
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }


        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                var user = _authService.AuthenticateUser(model.Email, model.Password);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "User" });
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.Signout();
            return RedirectToAction("LogOutComplete");
        }

        public IActionResult LogoutComplete()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult RestPassword()
        {
            return View();
        }

        public IActionResult Unauthorize()
        {
            return View();
        }

    }
}