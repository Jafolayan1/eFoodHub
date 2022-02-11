using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;
using eFoodHub.Repositories.Models;

using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        protected ApplicationDbContext Context
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
            return Context.Carts.Include("Items").Where(c => c.Id == CartId && c.IsActive == true).FirstOrDefault();
        }

        public int DeleteItem(Guid CartId, int ItemId)
        {
            CartItem item = Context.CartItems.Where(predicate: ci => ci.CartId == CartId && ci.ItemId == ItemId).FirstOrDefault();

            if (item != null)
            {
                Context.CartItems.Remove(item);
                return Context.SaveChanges();
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
                    if (cart.Items[i].ItemId == itemId)
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
                    return Context.SaveChanges();
            }
            return 0;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            Cart cart = GetCart(CartId);
            cart.UserId = UserId;
            return Context.SaveChanges();
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in Context.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Items = (from cartItem in Context.CartItems
                                      join item in Context.Items
                                      on cartItem.ItemId equals item.ItemId
                                      where cartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = cartItem.ItemId,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = cartItem.Quantity,
                                          ItemId = item.ItemId,
                                          UnitPrice = cartItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }
    }
}