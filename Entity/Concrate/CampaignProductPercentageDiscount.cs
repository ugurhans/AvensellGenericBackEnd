using Core.Entities;
using Entity.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate.paytr
{
    public class CampaignProductPercentageDiscount : IProductPercentageDiscountCampaign, IEntity
    {
        public int Id { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CombinedProduct { get; set; }
        public int? PercentageDiscountRate { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }

    }
}
