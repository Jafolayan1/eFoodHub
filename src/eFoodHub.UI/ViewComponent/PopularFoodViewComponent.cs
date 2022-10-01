using eFoodHub.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.ViewComponents
{
    public class PopularFoodViewComponent : ViewComponent
    {
        private readonly ICatalogService _catalogService;

        public PopularFoodViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            var items = _catalogService.GetItems().Where(f => f.IsPopular);
            return View("~/Views/Shared/_PopularFood.cshtml", items);
        }
    }
}