using Core.Entities;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Concrete
{
    public class Basket : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<BasketItem>? BasketItems { get; set; }


        public bool? IsCampaignApplied { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public int? CampaignId { get; set; }
        public decimal? CampaignDiscount { get; set; }


        public bool? IsCouponApplied { get; set; }
        public int? CouponId { get; set; }
        public decimal? CouponDiscount { get; set; }

    }
}

