using eFoodHub.Entities;
using eFoodHub.Repositories.Models;

namespace eFoodHub.Services.Interfaces
{
    public interface IOrderService
    {
        OrderModel GetOrderDetails(string OrderId);

        IEnumerable<Order> GetUserOrders(int UserId);

        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address);

        PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10);
    }
}