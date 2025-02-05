namespace Ecommerce.Services;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }  // Users table
    public DbSet<Product> Products { get; set; } 
    public DbSet<Cart> Cart { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<DeliverOrder> DeliverOrders { get; set; }

}




