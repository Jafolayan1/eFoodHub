﻿using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}