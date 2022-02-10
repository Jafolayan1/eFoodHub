using eFoodHub.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        private readonly ICatalogService _catalogService;

        public ItemController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            var data = _catalogService.GetItems();
            return View(data);
        }
    }
}