using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Contracts.v1._0.Responses
{
    public class Response<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
    }
}
