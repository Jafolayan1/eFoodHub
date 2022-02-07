using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Entities
{
    public class OrderItem
    {
        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(int itemId, decimal unitPrice, int quantity, decimal total)
        {
            ItemId = itemId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public int ItemId { get; set; }

        [Precision(8, 4)]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [Precision(8, 4)]
        public decimal Total { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}