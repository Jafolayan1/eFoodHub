using eFoodHub.Entities;

using Razorpay.Api;

namespace eFoodHub.Services.Interfaces
{
    public interface IRazorpayPaymentService
    {
        string CreateOrder(decimal amount, string currency, string receipt);

        string CapturePayment(string paymentId, string orderId);

        Payment GetPaymentDetails(string paymentId);

        bool VerifySignature(string signature, string orderId, string paymentId);

        int SavePaymentDetails(PaymentDetails model);
    }
}