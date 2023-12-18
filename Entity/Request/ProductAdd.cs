using System;
using Entity.Concrate;

namespace Entity.Request
{
    public class ProductAdd
    {
        public Product Product { get; set; }
        public FileUploadRequest? fileUpload { get; set; }
    }
}

