using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
