using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Data.Entities.IdentityModels;
using System.Reflection;

namespace OrderManagementSystem.Data.Data
{
    public class OrderDbContext : IdentityDbContext<AppCustomer>
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(assembly:Assembly.GetExecutingAssembly());  
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
