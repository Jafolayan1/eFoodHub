using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.User.Controllers
{
    [Area("User")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
