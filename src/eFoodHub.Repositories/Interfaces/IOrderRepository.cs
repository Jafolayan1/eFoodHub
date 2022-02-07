using eFoodHub.Entities;

namespace eFoodHub.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetUserOrders(int UderId);
    }
}