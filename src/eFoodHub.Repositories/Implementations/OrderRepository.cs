using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;
using eFoodHub.Repositories.Models;

using Microsoft.EntityFrameworkCore;

using X.PagedList;

namespace eFoodHub.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected ApplicationDbContext Context
        {
            get
            {
                return _dbContext as ApplicationDbContext;
            }
        }

        public OrderRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Order> GetUserOrders(int UderId)
        {
            return Context.Orders.Include(o => o.OrderItems).Where(u => u.UserId == UderId).ToList();
        }

        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in Context.Orders
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.DateCreated,
                             Items = (from orderItem in Context.OrderItems
                                      join item in Context.Items
                                      on orderItem.ItemId equals item.ItemId
                                      where orderItem.OrderId == orderId
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ItemId = item.ItemId,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in Context.Orders
                        join payment in Context.PaymentDetails
                        on order.PaymentId equals payment.Id
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = payment.Id,
                            CreatedDate = order.DateCreated,
                            GrandTotal = payment.GrandTotal,
                            Locality = order.Locality
                        });

            int itemCounts = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);

            StaticPagedList<OrderModel> pagedListData = new(orders, page, pageSize, itemCounts);

            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = pageSize;
            pagingModel.TotalRows = itemCounts;
            return pagingModel;
        }
    }
}