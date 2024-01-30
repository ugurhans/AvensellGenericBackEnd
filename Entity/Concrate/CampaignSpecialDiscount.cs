using Core.Entities;
using Entity.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class CampaignSpecialDiscount : ISpecialDiscountCampaign, IEntity  // o güne yada ana özel kişi için oluşturulan hediye ürün.
    {
        public int Id { get ; set ; }
        public string PromotionPeriodName { get; set; }  // kampanya adı bugüne özel anneler gününe özel vs
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProductID { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}
