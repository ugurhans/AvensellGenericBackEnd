using System;
using Core.Entities;
using Entity.Enum;

namespace Entity.Concrate
{
    public class Coupon : IEntity
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; } //indirim miktarı
        public int? MinBasketCost { get; set; } //min sepet tutarı
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CouponTypes couponTypes { get; set; }
        public string CouponImageUrl { get; set; }
        public string CouponName { get; set; }
    }
}

