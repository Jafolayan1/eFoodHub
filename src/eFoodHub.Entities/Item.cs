using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Precision(18, 4)]
        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int ItemTypeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ItemType ItemType { get; set; }
    }
}