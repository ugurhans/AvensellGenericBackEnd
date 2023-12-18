using System;
using Core;
using Entity.Enum;

namespace Entity.Dto
{
    public class CampaignDto : IDto
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CampaignDetail { get; set; }
        public string? Name { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public string? CampaignImageUrl { get; set; }
    }
}

