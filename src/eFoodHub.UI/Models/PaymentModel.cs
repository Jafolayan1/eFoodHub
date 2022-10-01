using eFoodHub.Repositories.Models;

namespace eFoodHub.UI.Models
{
    public class PaymentModel
    {
        public CartModel Cart { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal GrandTotal { get; set; }
        public string PayStackKey { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string CallbackUrl { get; set; }
        public string TrxReference { get; set; }
        public string Email { get; set; }
        public string Receipt { get; set; }
        public bool Status { get; set; }
    }
}