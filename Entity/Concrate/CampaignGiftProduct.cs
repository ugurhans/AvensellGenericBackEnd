using Core.Entities;
using Entity.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class CampaignGiftProduct : IGiftProductCampaign, IEntity
    {
        public int Id { get; set; }
        public int? GiftProductId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinPurchaseAmount { get; set; } //bu tutraın üstünde ürünü hediye et 
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}
