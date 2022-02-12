using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;
using eFoodHub.Services.Interfaces;
using eFoodHub.Services.Models;

using Microsoft.Extensions.Options;

using Razorpay.Api;

using System.Security.Cryptography;

using System.Text;

namespace eFoodHub.Services.Implementations
{
    public class RazorpayPaymentService : IRazorpayPaymentService
    {
        private readonly IOptions<RazorPayConfig> _razorPayConfig;
        private readonly RazorpayClient _client;
        private readonly IRepository<PaymentDetails> _paymentRepo;
        private readonly ICartRepository _cartRepo;

        public RazorpayPaymentService(IOptions<RazorPayConfig> razorPayConfig, IRepository<PaymentDetails> paymentRepo, ICartRepository cartRepo)
        {
            _razorPayConfig = razorPayConfig;
            _paymentRepo = paymentRepo;
            _cartRepo = cartRepo;
            if (_client == null)
            {
                _client = new RazorpayClient(_razorPayConfig.Value.Key, _razorPayConfig.Value.Secret);
            }
        }

        public string CreateOrder(decimal amount, string currency, string receipt)
        {
            try
            {
                Dictionary<string, object> options = new()
                {
                    { "amount", amount },
                    { "currency", currency },
                    { "receipt", receipt },
                    { "payment_capture", 1 }
                };
                Razorpay.Api.Order orderResponse = _client.Order.Create(options);
                return orderResponse["id"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string CapturePayment(string paymentId, string orderId)
        {
            if (!String.IsNullOrWhiteSpace(paymentId) && !String.IsNullOrWhiteSpace(orderId))
            {
                try
                {
                    Payment payment = _client.Payment.Fetch(paymentId);
                    // This code is for capture the payment
                    Dictionary<string, object> options = new()
                    {
                        { "amount", payment.Attributes["amount"] },
                        { "currency", payment.Attributes["currency"] }
                    };
                    Payment paymentCaptured = payment.Capture(options);
                    return paymentCaptured.Attributes["status"];
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            if (!String.IsNullOrWhiteSpace(paymentId))
            {
                return _client.Payment.Fetch(paymentId);
            }
            return null;
        }

        public bool VerifySignature(string signature, string orderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", orderId, paymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = GetActualSignature(payload, secret);
            return actualSignature.Equals(signature);
        }

        private static string GetActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public int SavePaymentDetails(PaymentDetails model)
        {
            _paymentRepo.Add(model);
            var cart = _cartRepo.GetById(model.CartId);
            cart.IsActive = false;
            return _paymentRepo.SaveChanges();
        }
    }
}