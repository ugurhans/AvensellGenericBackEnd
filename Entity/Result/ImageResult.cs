using System;
namespace Entity.Result
{
    public class ImageResult
    {
        public string Url { get; set; }

        public ImageResult(string url)
        {
            Url = url;
        }
    }
}

