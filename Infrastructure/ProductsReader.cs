using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;

namespace FurnitureStore3.Infrastructure
{
    public class ProductsReader : IProductsReader
    {
        private readonly IRepository<Product> repository;
        private readonly IRepository<Category> categories;

        public ProductsReader(IRepository<Product> products, IRepository<Category> categories)
        {
            this.repository = products;
            this.categories = categories;
        }
        public async Task<Product?> FindProductAsync(int ProductId) =>
    await repository.FindAsync(ProductId);
        public async Task<List<Product>> GetAllProductsAsync() => await repository.GetAllAsync();
        public async Task<List<Product>> FindProductsAsync(string searchString, int categoryId) => (searchString, categoryId) switch
        {
            ("" or null, 0) => await repository.GetAllAsync(),
            (_, 0) => await repository.FindWhere(product => product.Name.Contains(searchString)),
            (_, _) => await repository.FindWhere(product => product.CategoryId == categoryId &&
                (product.Name.Contains(searchString))),
        };
        public async Task<List<Category>> GetCategoriesAsync() => await categories.GetAllAsync();
    }
}
