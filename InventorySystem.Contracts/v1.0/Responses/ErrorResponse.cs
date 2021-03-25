using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
