using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.Domain.Services
{
    public interface IOrdersReader
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Product>> FindProductsAsync(string searchString, int categoryId);
        Task<Product?> FindProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Category>> GetCategoriesAsync();
    }
}
