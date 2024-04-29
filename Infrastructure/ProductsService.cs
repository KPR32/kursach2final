using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;

namespace FurnitureStore3.Infrastructure
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> products;

        public ProductsService(IRepository<Product> products)
        {
            this.products = products;
        }

        public async Task AddProduct(Product product)
        {
            await products.AddAsync(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await products.DeleteAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await products.UpdateAsync(product);
        }


        private async Task CopyFromStream(Stream stream, string filename)
        {
            using (var writer = new FileStream(filename, FileMode.Create))
            {
                int count = 0;
                byte[] buffer = new byte[1024];
                do
                {
                    count = await stream.ReadAsync(buffer, 0, buffer.Length);
                    await writer.WriteAsync(buffer, 0, count);

                } while (count > 0);
            }
        }

        public async Task<string> LoadFile(Stream file, string path)
        {
            var filename = Path.GetRandomFileName() + ".pdf";
            var fullname = Path.Combine(path, filename);

            await CopyFromStream(file, fullname);

            return filename;
        }

        public async Task<string> LoadPhoto(Stream file, string path)
        {
            var filename = Path.GetRandomFileName() + ".png";
            var fullname = Path.Combine(path, filename);

            await CopyFromStream(file, fullname);

            return filename;
        }

    }
}
