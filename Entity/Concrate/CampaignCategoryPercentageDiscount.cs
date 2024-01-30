﻿using Core.Entities;
using Entity.Abtract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class CampaignCategoryPercentageDiscount : ICategoryPercentageDiscountCampaign, IEntity
    {
        public int Id { get ; set ; }
        public int? CategoryId { get; set; }
        public decimal? MinPurchaseAmount { get; set; }  //min sepet tutarı 
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       // public int CategoryPercentageDiscountCampaignId { get; set; }
        public int? PercentageDiscountRate { get; set; }  // indirim yüzdeilk oranı 
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}
