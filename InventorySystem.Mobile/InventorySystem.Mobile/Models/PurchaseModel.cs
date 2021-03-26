using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Mobile.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
