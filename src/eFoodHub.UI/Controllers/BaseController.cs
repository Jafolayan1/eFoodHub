using eFoodHub.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<User> _userManager;

        public User CurrentUser
        {
            get
            {
                if (User.Identity.Name != null)
                    return _userManager.FindByNameAsync(User.Identity.Name).Result;
                else
                    return null;
            }
        }

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}