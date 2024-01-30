using System;
using System.Runtime;
using Core;
using Entity.Enum;

namespace Entity.Dto
{
    public class CampaignAddDto : IDto
    {

        public int? ProductFirstId { get; set; }
        public int? ProductSecondId { get; set; }
        public int? ProductGift { get; set; }
        public decimal? ProductSecondDiscount { get; set; }
        public int? MaxDiscountAmount { get; set; }
        public decimal? ProductFirstDiscount { get; set; }
        public double? Discount { get; set; }
        public List<int>? ProductIds { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public int? MinPurchaseAmount { get; set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? GiftProductId { get; set; }
        public string? PromotionPeriodName { get; set; }
        public List<int>? CombinedProductIds { get; set; }
        public int? PercentageDiscountRate { get; set; }
        public string CombinedProduct { get; set; }  //bu ürünler alınıgı zaman

    }
}
