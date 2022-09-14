using eFoodHub.Services.Interfaces;
using eFoodHub.UI.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace eFoodHub.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            var items = _catalogService.GetItems();
            return View(items);
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}