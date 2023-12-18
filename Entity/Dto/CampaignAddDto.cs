using System;
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
        public decimal? ProductFirstDiscount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CampaignTypes? CampaignType { get; set; }
    }
}

