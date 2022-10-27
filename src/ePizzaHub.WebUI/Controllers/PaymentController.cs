using ePizzaHub.Entities;
using ePizzaHub.Repositories.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.Services.Models;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using ePizzaHub.WebUI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using PayStack.Net;

using System;
using System.Text.Json;

namespace ePizzaHub.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IOptions<PaystackConfig> _paystackConfig;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly PayStackApi _client;

        public PaymentController(IPaymentService paymentService, IOptions<PaystackConfig> paystackConfig, IUserAccessor userAccessor, IOrderService orderService) : base(userAccessor)
        {
            _paystackConfig = paystackConfig;
            _paymentService = paymentService;
            _orderService = orderService;
            if (_client == null)
            {
                _client = new PayStackApi(_paystackConfig.Value.Key);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            PaymentModel payment = new PaymentModel();
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
            payment.Receipt = Guid.NewGuid().ToString();

            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(PaymentModel model)
        {
            decimal amount = Convert.ToDecimal(model.GrandTotal * 100);
            string email = model.Email;
            string currency = model.Currency;
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
