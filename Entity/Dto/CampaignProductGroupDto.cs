using System;
using Core;

namespace Entity.Dto
{
    public class CampaignProductGroupDto : IDto
    {
        public int Id { get; set; }
        public int ProductFirstId { get; set; }
        public string ProductFirstName { get; set; }
        public int ProductSecondId { get; set; }
        public string ProductSecondName { get; set; }
        public decimal? ProductFirstDiscount { get; set; }
        public decimal? ProductSecondDiscount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

