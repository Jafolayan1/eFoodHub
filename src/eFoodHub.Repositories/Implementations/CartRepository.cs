using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context
        {
            get
            {
                return _dbContext as ApplicationDbContext;
            }
        }

        public CartRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Cart GetCart(Guid CartId)
        {
            return _context.Carts.Include("Items").Where(c => c.Id == CartId && c.IsActive == true).FirstOrDefault();
        }

        public int DeleteItem(Guid CartId, int ItemId)
        {
            var item = _context.CartItems.Where(ci => ci.CartId == CartId && ci.Id == ItemId).FirstOrDefault();

            if (item != null)
            {
                _context.CartItems.Remove(item);
                return _context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateQuantity(Guid CartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(CartId);
            if (cart != null)
            {
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].Id == itemId)
                    {
                        flag = true;
                        //for minus quantity
                        if (Quantity < 0 && cart.Items[i].Quantity > 1)
                            cart.Items[i].Quantity += (Quantity);
                        else if (Quantity > 0)
                            cart.Items[i].Quantity += (Quantity);
                        break;
                    }
                }
                if (flag)
                    return _context.SaveChanges();
            }
            return 0;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            Cart cart = GetCart(CartId);
            cart.UserId = UserId;
            return _context.SaveChanges();
        }
    }
}