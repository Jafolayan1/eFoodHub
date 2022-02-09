using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
