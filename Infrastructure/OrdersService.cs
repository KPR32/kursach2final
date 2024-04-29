using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;

namespace FurnitureStore3.Infrastructure
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> orders;

        public OrdersService(IRepository<Order> orders)
        {
            this.orders = orders;
        }

        public async Task AddOrder(Order order)
        {
            await orders.AddAsync(order);
        }
    }
}
