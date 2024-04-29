using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.Domain.Services
{
    public interface IOrdersService
    {
        Task AddOrder(Order order);
    }
}
