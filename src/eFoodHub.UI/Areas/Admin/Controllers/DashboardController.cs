using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
