using eFoodHub.Repositories.Models;
using eFoodHub.UI.Helpers;
using eFoodHub.UI.Models;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            RazorPaymentModel payment = new();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }
            payment.GrandTotal = Math.Round(cart.GrandTotal);
            payment.Currency = "$";
            String items = "";
            foreach (var item in cart.Items)
            {
                items += item.Name + ",";
            }
            payment.Description = items;
            return View(payment);
        }
    }
}