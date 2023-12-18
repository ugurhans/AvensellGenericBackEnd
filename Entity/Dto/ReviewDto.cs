using System;
using Core;

namespace Entity.Dto
{
    public class ReviewDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public decimal Rating { get; set; }
    }
}

