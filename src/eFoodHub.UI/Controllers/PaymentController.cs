using eFoodHub.Entities;
using eFoodHub.Repositories.Models;
using eFoodHub.Services.Interfaces;
using eFoodHub.Services.Models;
using eFoodHub.UI.Helpers;
using eFoodHub.UI.Interfaces;
using eFoodHub.UI.Models;
using eFoodHub.WebUI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using PayStack.Net;

using System.Text.Json;

namespace eFoodHub.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IOptions<PayStackConfig> _payStackConfig;
        private readonly PayStackApi _client;

        public PaymentController(IPaymentService paymentService, IOptions<PayStackConfig> payStackConfig, IUserAccessor userAccessor, IOrderService orderService) : base(userAccessor)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _payStackConfig = payStackConfig;
            if (_client == null)
            {
                _client = new PayStackApi(_payStackConfig.Value.Key);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            PaymentModel payment = new();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }
            payment.GrandTotal = Math.Round(cart.GrandTotal);
            string items = "";
            foreach (var item in cart.Items)
            {
                items += item.Name + ",";
            }
            payment.Description = items;
            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            decimal amount = Convert.ToDecimal(form["amount"]) * 100;
            string email = form["email"];
            string currency = form["currency"];
            var response = _paymentService.MakePayment(amount, email, currency);
            if (response.Status)
            {
                TempData["reference"] = response.Data.Reference;
                return Redirect(response.Data.AuthorizationUrl);
            }
            ViewData["error"] = "One or more errors occured";
            return View(typeof(Index));
        }

        [HttpGet]
        public IActionResult Verify(string reference)
        {
            try
            {
                var response = _client.Transactions.Verify(reference);

                var payment = JsonSerializer.Deserialize<Rootobject>(response.RawJson);

                var paymentId = payment.data.id.ToString();

                if (response.Data.Status == "success")
                {
                    CartModel cart = TempData.Get<CartModel>("Cart");
                    PaymentDetails model = new()
                    {
                        CartId = cart.Id,
                        Total = cart.Total,
                        Tax = cart.Tax,
                        GrandTotal = cart.GrandTotal,

                        Status = response.Data.Status,
                        TransactionId = paymentId,
                        Currency = response.Data.Currency,
                        Email = response.Data.Customer.Email,
                        Id = Guid.NewGuid().ToString(),
                        UserId = CurrentUser.Id
                    };

                    int status = _paymentService.SavePaymentDetails(model);
                    if (status > 0)
                    {
                        Response.Cookies.Append("CId", "");

                        Address address = TempData.Get<Address>("Address");
                        _orderService.PlaceOrder(CurrentUser.Id, paymentId, paymentId, cart, address);

                        //TO DO: Send email
                        TempData.Set("PaymentDetails", model);
                        return RedirectToAction("Receipt");
                    }
                    else
                    {
                        ViewBag.Message = "Due to some technical issues it's not get updated in our side. We will contact you soon..";
                        ViewData["error"] = response.Data.GatewayResponse;
                    }
                }
            }
            catch (Exception)
            {
            }
            ViewData["error"] = "Your payment has been failed. You can contact us at support@craveordering.com.";
            return View(nameof(Index));
        }

        public IActionResult Receipt()
        {
            PaymentDetails model = TempData.Peek<PaymentDetails>("PaymentDetails");
            return View(model);
        }

        public class Rootobject
        {
            public bool status { get; set; }
            public string dessage { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public ulong id { get; set; }
        }
    }
}