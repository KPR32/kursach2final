using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.Data
{
    public class EFInitialSeed
    {
        public static void Seed(FurnitureContext context)
        {
            if (!context.Products.Any() && !context.Categories.Any())
            {
                Category system = new Category
                {
                    Name = "КРОВАТтттЬ",
                    Products = new List<Product>()
            {
                
                new Product
                {
                    Name = "Кровать",
                    Weight = 12,
                    Price = 1990,
                    Description = "Чет про кровать\r\nс",
                    ImageUrl = "product1.png",
                }
            }
                };                
                context.Categories.Add(system);                
                context.SaveChanges();
            }
        }
    }
}
