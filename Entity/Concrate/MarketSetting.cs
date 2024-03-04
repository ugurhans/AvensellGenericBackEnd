using Core.Entities;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public List<MarketSettingItem>? DeliveryandPaymentOptions { get; set; } // Liste olarak tanımlandı
        public string? PrimaryText { get; set; }
        public string? Secondary { get; set; }
        public string? Text { get; set; }
        public string? Background { get; set; }
        public string? Error { get; set; }
        public string? Warning { get; set; }

    }

    public class MarketSettingItem : IEntity
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public bool CashOnDelivery { get; set; }
        public bool CreditCardOnDelivery { get; set; }
        public bool OnlinePayment { get; set; }
        public bool Ring { get; set; }
        public bool Door { get; set; }
        public bool Pickup { get; set; }
    }
}
