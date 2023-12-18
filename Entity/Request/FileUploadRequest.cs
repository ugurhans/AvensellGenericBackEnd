using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entity.Request
{
    public class FileUploadRequest : IEntity
    {
        public int Id { get; set; }
        public string Collection { get; set; }
        public int OwnerId { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public int UserId { get; set; }
    }
}
