using System;
using Core.Entities;

namespace Entity.Concrete
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int OrderBy { get; set; }
        public string Type { get; set; }
        public bool? IsActive { get; set; }
    }
}

