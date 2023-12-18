using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Result
{
    public class ProductResult
    {
        public ProductResult(string message, int? id, bool Success)
        {
            this.Message = message;
            this.id = id;
            this.Success = Success;
        }

        public ProductResult(string message, bool Success)
        {
            this.Success = Success;
            this.Message = message;
        }

        public int? id { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

    }
}
