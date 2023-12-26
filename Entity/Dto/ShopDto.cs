using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class ShopDto:IDto
    {
        public int ShopId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Mail { get; set; }
        public string? Website { get; set; }
        public string? Whatsapp { get; set; }
        public string? LogoURL { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? MinimumBasketPrice { get; set; }
        public decimal? DeliveryFee { get; set; }
        public bool? CouponEnabled { get; set; }
        public bool? CampaignEnabled { get; set; }
    }
}
