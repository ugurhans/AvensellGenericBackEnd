using Core;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class ProductSimpleDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int OrderBy { get; set; }
        public int UnitType { get; set; }
        public int UnitQuantity { get; set; }
        public int UnitCount { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PaidPrice { get; set; }
        public string BrandName { get; set; }
    }
}
