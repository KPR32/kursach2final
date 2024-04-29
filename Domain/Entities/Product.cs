using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.Domain.Entities
{
    public class Product : Entity
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        public double? Weight { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
