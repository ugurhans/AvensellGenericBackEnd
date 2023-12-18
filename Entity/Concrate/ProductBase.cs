using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class ProductBase : IEntity
    {
        public int Id { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? UnitType { get; set; }
        public int? UnitQuantity { get; set; }
        public int? UnitCount { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
    }
}

