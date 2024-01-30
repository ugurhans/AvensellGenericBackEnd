using System;
using Core.Entities;
using Entity.Abtract;

namespace Entity.Concrate
{
    public class CampaignGift : IGiftCampaign, IEntity
    {
        public int Id { get; set; }
        public int ProductFirstId { get; set; }
        public int ProductSecondId { get; set; }
        public int? ProductGift { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}

