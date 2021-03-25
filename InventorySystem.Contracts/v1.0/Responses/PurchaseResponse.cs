using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Responses
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
