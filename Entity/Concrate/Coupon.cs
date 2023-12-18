using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class Coupon : IEntity
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public decimal Discount { get; set; }
        public int MinBasketCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

       
    }
}

