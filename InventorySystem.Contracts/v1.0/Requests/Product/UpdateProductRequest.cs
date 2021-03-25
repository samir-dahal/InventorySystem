using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Product
{
    public class UpdateProductRequest
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
    }
}
