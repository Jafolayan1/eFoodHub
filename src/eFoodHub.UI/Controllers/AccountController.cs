using eFoodHub.Entities;
using eFoodHub.Services.Interfaces;
using eFoodHub.UI.Models;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationService _authService;
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = new()
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            bool result = _authService.CreateUser(user, model.Password);
            if (result)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }

    }
}