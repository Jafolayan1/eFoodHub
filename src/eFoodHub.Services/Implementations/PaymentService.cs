using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;
using eFoodHub.Services.Interfaces;
using eFoodHub.Services.Models;

using Microsoft.Extensions.Options;

using PayStack.Net;

namespace ePizzaHub.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IOptions<PayStackConfig> _payStackConfig;
        private readonly PayStackApi _client;

        private IRepository<PaymentDetails> _paymentRepo;
        private ICartRepository _cartRepo;

        public PaymentService(IOptions<PayStackConfig> payStackConfig, IRepository<PaymentDetails> paymentRepo, ICartRepository cartRepo)
        {
            _payStackConfig = payStackConfig;
            _paymentRepo = paymentRepo;
            _cartRepo = cartRepo;
            if (_client == null)
            {
                _client = new PayStackApi(_payStackConfig.Value.Key);
            }
        }

        public TransactionInitializeResponse MakePayment(decimal amount, string email, string currency)
        {
            try
            {
                TransactionInitializeRequest request = new()
                {
                    AmountInKobo = (int)amount,
                    Email = email,
                    Currency = currency,
                    Reference = Generate().ToString(),
                    CallbackUrl = "https://localhost:7080/payment/verify"
                };

                TransactionInitializeResponse response = _client.Transactions.Initialize(request);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TransactionFetchResponse GetPaymentDetails(string transactionId)
        {
            if (!String.IsNullOrWhiteSpace(transactionId))
            {
                return _client.Transactions.Fetch(transactionId);
            }
            return null;
        }

        public int SavePaymentDetails(PaymentDetails model)
        {
            _paymentRepo.Add(model);
            var cart = _cartRepo.GetCart(model.CartId);
            cart.IsActive = false;
            return _paymentRepo.SaveChanges();
        }

        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }
    }
}