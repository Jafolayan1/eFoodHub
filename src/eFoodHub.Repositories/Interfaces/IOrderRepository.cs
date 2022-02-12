using eFoodHub.Entities;
using eFoodHub.Repositories.Models;

namespace eFoodHub.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int UderId);

        OrderModel GetOrderDetails(string id);

        PagingListModel<OrderModel> GetOrderList(int page, int pageSize);
    }
}