using System;
using Core;

namespace Entity.Dto
{
    public class BasketProductSimpleDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public int UnitType { get; set; }
        public decimal UnitQuantity { get; set; }
        public int UnitCount { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitDiscount { get; set; }
        public decimal UnitPaidPrice { get; set; }
        public int ProductCount { get; set; }
        public int OrderBy { get; set; }
    }
}

