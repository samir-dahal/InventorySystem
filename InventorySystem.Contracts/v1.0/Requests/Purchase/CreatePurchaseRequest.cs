using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Purchase
{
    public class CreatePurchaseRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
