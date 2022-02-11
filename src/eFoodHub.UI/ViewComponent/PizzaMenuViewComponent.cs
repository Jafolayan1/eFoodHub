using eFoodHub.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {
        private readonly ICatalogService _catalogService;

        public PizzaMenuViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            var items = _catalogService.GetItems();
            return View("~/Views/Shared/_PizzaMenu.cshtml", items);
        }
    }
}