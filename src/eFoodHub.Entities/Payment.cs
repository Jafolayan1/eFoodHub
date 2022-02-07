using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Entities
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }

        [Precision(8, 4)]
        public decimal Tax { get; set; }

        public string Currency { get; set; }

        [Precision(8, 4)]
        public decimal Total { get; set; }

        public string Email { get; set; }
        public string Status { get; set; }
        public Guid CartId { get; set; }
        public int UserId { get; set; }

        [Precision(8, 4)]
        public decimal GrandTotal { get; set; }
    }
}