using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Category
{
    public class UpdateCategoryRequest
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
