using Core.Entities;
using Entity.Abtract;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class CouponCategory : ICategoryCoupon, IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int MinBasketCost { get; set; } //min sepet tutarı
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CouponImageUrl { get; set; }
    }
}
