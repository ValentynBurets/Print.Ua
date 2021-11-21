using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.EF
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Order
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Customer)
                .WithMany(b => b.MyOrdersAsCustomer)
                .HasForeignKey(k => k.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Employee)
                .WithMany(b => b.MyOrdersAsEmployee)
                .IsRequired(false)
                .HasForeignKey(k => k.EmployeeId);

            modelBuilder.Entity<Order>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Employee
            modelBuilder.Entity<Employee>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Customer
            modelBuilder.Entity<Customer>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Admin
            modelBuilder.Entity<Admin>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //Comment
            modelBuilder.Entity<OrderComment>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            modelBuilder.Entity<OrderComment>()
                .HasOne(o => o.Order)
                .WithMany(m => m.MyComments)
                .HasForeignKey(k => k.OrderId);

            //ProductProductService
            modelBuilder.Entity<Product>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            modelBuilder.Entity<Product>()
                .HasOne(o => o.ServiceInProduct)
                .WithMany(m => m.Products)
                .HasForeignKey(k => k.ServiceId);

            modelBuilder.Entity<Product>()
                .HasOne(o => o.ContainingOrder)
                .WithMany(m => m.MyProducts)
                .HasForeignKey(k => k.OrderId);

            //Service
            modelBuilder.Entity<ProductService>()
                .HasOne(o => o.Material)
                .WithMany()
                .HasForeignKey(k => k.MaterialId);

            modelBuilder.Entity<ProductService>()
                .HasOne(o => o.Format)
                .WithMany()
                .HasForeignKey(m => m.FormatId);

            modelBuilder.Entity<ProductService>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //ServiceFormat
            modelBuilder.Entity<ServiceFormat>()
                .HasIndex(i => i.Id)
                .IsUnique(true);

            //ServiceMaterial
            modelBuilder.Entity<ServiceMaterial>()
                .HasIndex(i => i.Id)
                .IsUnique(true);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderComment> OrderComments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductService> Services { get; set; }
        public DbSet<ServiceFormat> ServiceFormats { get; set; }
        public DbSet<ServiceMaterial> ServiceMaterials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
