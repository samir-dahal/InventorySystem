using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Responses
{
    public class SupplierResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
