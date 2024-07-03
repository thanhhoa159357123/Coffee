using Microsoft.EntityFrameworkCore;

namespace CoffeeStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private CoffeeDbContext context;
        public EFOrderRepository(CoffeeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
        .Include(o => o.Lines)
        .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }

}
