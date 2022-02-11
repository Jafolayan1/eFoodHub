using eFoodHub.Entities;
using eFoodHub.Repositories.Models;

namespace eFoodHub.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(Guid CartId);

        CartModel GetCartDetails(Guid CartId);

        int DeleteItem(Guid CartId, int ItemId);

        int UpdateQuantity(Guid CartId, int itemId, int Quantity);

        int UpdateCart(Guid CartId, int UserId);
    }
}