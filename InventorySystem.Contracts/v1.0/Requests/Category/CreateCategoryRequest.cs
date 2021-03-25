using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Requests.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
