using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Entity.DTOs
{
    public class FileResponseDto : IDto
    {
        public string Data { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string DataType { get; set; }
        public string? Uri { get; set; }
    }
}
