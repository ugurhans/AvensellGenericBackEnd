using Core.Entities;
using Entity.Concrate;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BasketId { get; set; }

        public decimal? TotalOrderDiscount { get; set; }
        public decimal? TotalOrderPrice { get; set; }
        public decimal? TotalOrderPaidPrice { get; set; }


        public DateTime? OrderDate { get; set; }
        public OrderStates? State { get; set; }

        public DateTime? ConfirmDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public PaymentTypes? PaymentType { get; set; }
        public CallRings? CallRing { get; set; }
        public decimal? DeliveryFee { get; set; }


        public bool? IsCampaignApplied { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public int? CampaignId { get; set; }
        public decimal? CampaignDiscount { get; set; }

        public int AddressId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        // bunlar dbb ye eklendi
        public DateTime? PaymentApprovedDate { get; set; }
        public int OrderContactId { get; set; } 

    }
}
