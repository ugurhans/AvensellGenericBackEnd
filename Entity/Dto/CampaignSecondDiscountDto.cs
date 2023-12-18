using System;
using Core;

namespace Entity.Dto
{
    public class CampaignSecondDiscountDto : IDto
    {
        public int Id { get; set; }
        public int ProductFirstId { get; set; }
        public string ProductFirstName { get; set; }
        public decimal? ProductSecondDiscount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

