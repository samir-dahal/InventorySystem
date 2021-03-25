using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Sale
{
    public class CreateSaleRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
