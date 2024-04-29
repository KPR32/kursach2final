using FurnitureStore3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore3.Data
{
    public class FurnitureContext : DbContext
    {
        public FurnitureContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
