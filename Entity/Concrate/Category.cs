using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public int OrderBy { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}

