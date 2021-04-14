using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace WebShop_NULL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ImageMetadata> ImageMetadata { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<City> Cities { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}