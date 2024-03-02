using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Entity.Concrate
{
    public class MarketSetting: IEntity
    {
        public int Id { get; set; }
        public string? MarketName { get; set; }
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
        public string? DeliveryOptions { get; set; }
        public string? PaymentOptions { get; set; }
        public string? PrimaryText { get; set; }
        public string? Secondary { get; set; }
        public string? Text { get; set; }
        public string? Background { get; set; }
        public string? Error { get; set; }
        public string? Warning { get; set; }

    }
}
