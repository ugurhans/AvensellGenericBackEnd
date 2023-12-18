using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class UserCoupon : IEntity
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int UserId { get; set; }
        public int BasketId { get; set; }
        public DateTime UsageDate { get; set; }
    }
}

