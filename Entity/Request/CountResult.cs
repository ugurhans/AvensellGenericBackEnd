using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Request
{
    public class CountResult
    {
        public CountResult(string message, int? Count, bool Success)
        {
            this.Message = message;
            this.Count = Count;
            this.Success = Success;
        }

        public CountResult(string message, bool Success)
        {
            this.Success = Success;
            this.Message = message;
        }

        public int? Count { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

    }
}
