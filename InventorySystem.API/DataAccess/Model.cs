using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//created this single model class to visualize the table relationships
//can be moved to respective .cs file if needed
namespace InventorySystem.API.DataAccess
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
    }
    public class Purchase
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        [Required]
        [Column(TypeName = "decimal(20,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(20,2")]
        public decimal TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }

    }
    public class Sale
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [Column(TypeName = "decimal(20,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(20,2")]
        public decimal TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
