using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.Domain.Entities
{
    public class Category : Entity
    {
        [StringLength(150)]
        public string Name { get; set; } = null!;
        public List<Product> Products { get; set; }
    }
}
