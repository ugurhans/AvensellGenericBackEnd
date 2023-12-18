using System;
using Core.Entities;
using Entity.Abtract;

namespace Entity.Concrate
{

    public class CampaignSecondDiscount : ISecondDiscountCampaign, IEntity
    {
        public int Id { get; set; }
        public int ProductFirstId { get; set; }
        public decimal? ProductSecondDiscount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

