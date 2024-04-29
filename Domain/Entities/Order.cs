using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.Domain.Entities
{
    public class Order : Entity
    {
        public int ClientUserId { get; set; }        
        public int ProductId { get; set; }        
        public int Price { get; set; }
        public DateTime OrderStart { get; set; }
        public DateTime OrderFinish { get; set; }

        [StringLength(100)]
        public string OrderAddress { get; set; } = null!;
    }
}
