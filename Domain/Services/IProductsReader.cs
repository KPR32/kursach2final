using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.Domain.Services
{
    public interface IProductsReader
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> FindProductsAsync(string searchString, int categoryId);
        Task<Product?> FindProductAsync(int productId);
        Task<List<Category>> GetCategoriesAsync();
    }
}
