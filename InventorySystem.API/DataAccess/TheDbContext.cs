using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.API.DataAccess
{
    public class TheDbContext : DbContext
    {
        public TheDbContext(DbContextOptions<TheDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //restrict category deletion, if there is any product to this category
            modelBuilder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //restrict supplier deletion, if there is any purchase to this supplier
            modelBuilder.Entity<Purchase>()
                .HasOne(purchase => purchase.Supplier)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //restrict customer deletion, if there is any sale to this customer
            modelBuilder.Entity<Sale>()
                .HasOne(sale => sale.Customer)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //restrict product deletion, if there is any purchase to this product
            modelBuilder.Entity<Purchase>()
                .HasOne(purchase => purchase.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //restrict purchase deletion, if there is any sale to this purchase
            modelBuilder.Entity<Sale>()
                .HasOne(sale => sale.Purchase)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}