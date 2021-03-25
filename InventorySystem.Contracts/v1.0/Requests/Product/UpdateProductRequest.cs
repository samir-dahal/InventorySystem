using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Product
{
    public class UpdateProductRequest
    {
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
