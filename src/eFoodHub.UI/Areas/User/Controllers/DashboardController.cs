using eFoodHub.UI.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : BaseController
    {
        public DashboardController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}