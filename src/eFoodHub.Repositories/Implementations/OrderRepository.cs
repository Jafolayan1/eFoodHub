using eFoodHub.Entities;
using eFoodHub.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected ApplicationDbContext _context
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
            return _context.Orders.Include(o => o.OrderItems).Where(u => u.UserId == UderId).ToList();
        }
    }
}