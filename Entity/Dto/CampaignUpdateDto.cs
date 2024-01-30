using Core;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class CampaignUpdateDto:IDto
    {
        public int Id { get; set; }
        public int? ProductFirstId { get; set; }
        public int? ProductSecondId { get; set; }
        public int? ProductGift { get; set; }
        public decimal? ProductSecondDiscount { get; set; }
        public decimal? ProductFirstDiscount { get; set; }


        public int CombinedDiscountCampaignId { get; set; }
        public int SpecialDiscountCampaignId { get; set; }
        public int CategoryPercentageDiscountCampaignId { get; set; }
        public int ProductGroupCampaignID { get; set; }
        public int GiftProductCampaignID { get; set; }
        public int ProductPercentageDiscountCampaignId { get; set; }
        public int GiftCampaignID { get; set; }
        public int SecondDiscountCampaignId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CampaignDetail { get; set; }
        public string CampaignImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string CampaignName { get; set; }
        public double Discount { get; set; }//
        public CampaignTypes? CampaignType { get; set; }
        public List<int> ProductIds { get; set; }
        public int MinPurchaseAmount { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int GiftProductId { get; set; }
        public string PromotionPeriodName { get; set; }
        public int MaxDiscountAmount { get; set; }
        public List<int> CombinedProductIds { get; set; }
        public string CombinedProduct { get; set; }
        public int PercentageDiscountRate { get; set; }
    }
}
