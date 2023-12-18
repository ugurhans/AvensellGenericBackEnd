using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class SubCategory : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int OrderBy { get; set; }
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}

