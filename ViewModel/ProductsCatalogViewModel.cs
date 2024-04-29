using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.ViewModels
{
    
    public class ProductsCatalogViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }
        public List<User> Users { get; set; }
    }
}
