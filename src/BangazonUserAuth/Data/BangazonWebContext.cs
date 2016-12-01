using Microsoft.EntityFrameworkCore;
using BangazonUserAuth.Models;

namespace BangazonUserAuth.Data
{
    //Class Name: BangazonUserAuthContext
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to create a context in memory for our Controllers to interact with our Database.
    //Methods in Class: No Methods but DBSets of Customer, LineItem, Order, PaymentType, Product, ProductType
    public class BangazonUserAuthContext : DbContext
    {
        public BangazonUserAuthContext(DbContextOptions<BangazonUserAuthContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<LineItem> LineItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<SubProductType> SubProductType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
// When an Order is being created we are setting a DateCreated property here of current: Year, Month, Day, Hour, Minute, and Second.
            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

// When a Product is being created we are setting a DateCreated property here of current: Year, Month, Day, Hour, Minute, and Second.
            modelBuilder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }
    }
}