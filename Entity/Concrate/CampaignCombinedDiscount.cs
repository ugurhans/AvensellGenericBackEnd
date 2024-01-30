using Core.Entities;
using Entity.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class CampaignCombinedDiscount : ICombinedDiscountCampaign, IEntity
    {
        public int Id { get ; set ; }
        public decimal? MaxDiscountAmount { get; set; } //max 1000 tl ye kadar gecerli 
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CombinedProduct { get; set; }  //bu ürünler alınıgı zaman
        public int? PercentageDiscountRate { get; set; }  // indirim yüzdeilk oranı 
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}
