using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.Domain.Entities
{
    public class User : Entity
    {
        [StringLength(100)]
        public string Fullname { get; set; } = null!;

        [StringLength(10)]
        public string Phone { get; set; } = null!;

        [StringLength(100)]
        public string Login { get; set; } = null!;

        [StringLength(256)]
        public string Password { get; set; } = null!;
        
        [StringLength(100)]
        public string Salt { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
