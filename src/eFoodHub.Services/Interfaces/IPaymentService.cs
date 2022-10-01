using eFoodHub.Entities;

using PayStack.Net;

namespace eFoodHub.Services.Interfaces
{
    public interface IPaymentService
    {
        TransactionInitializeResponse MakePayment(decimal amount, string email, string currency);

        TransactionFetchResponse GetPaymentDetails(string transactionId);

        int SavePaymentDetails(PaymentDetails model);
    }
}