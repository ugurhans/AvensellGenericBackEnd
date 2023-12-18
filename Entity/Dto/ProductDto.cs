using System;
using Core;
using Entity.Concrate;

namespace Entity.Dto
{
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public decimal Discount { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public int OrderBy { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int UnitType { get; set; }
        public int UnitQuantity { get; set; }
        public int UnitCount { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Manufacturer { get; set; }
        public double? Rating { get; set; }
        public bool? IsFavorite { get; set; }
        public decimal? PaidPrice { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }

        public List<Review>? Reviews { get; set; }

    }
}

