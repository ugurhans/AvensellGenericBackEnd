using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Entity.Concrate;

namespace Entity.Dtos
{
    public class BasketItemDto : IDto
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }


        public decimal UnitPrice { get; set; }
        public decimal UnitDiscount { get; set; }
        public decimal UnitPaidPrice { get; set; }
        public int? ProductCount { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal TotalPaidPrice { get; set; }
        public decimal TotalDiscount { get; set; }



        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int UnitType { get; set; }
        public int UnitQuantity { get; set; }
        public int UnitCount { get; set; }

        public string ImageUrl { get; set; }


    }
}
