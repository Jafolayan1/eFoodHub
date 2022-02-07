using eFoodHub.Entities;

namespace eFoodHub.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(Guid CartId);

        int DeleteItem(Guid CartId, int ItemId);

        int UpdateQuantity(Guid CartId, int itemId, int Quantity);

        int UpdateCart(Guid CartId, int UserId);
    }
}