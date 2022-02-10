using eFoodHub.UI.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}