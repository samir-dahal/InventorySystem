using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Responses
{
    public class SaleResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PurchaseId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
