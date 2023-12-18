using System;
using Core;
using Entity.Concrete;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;


namespace Entity.Dto
{
    public class OrderDto : IDto
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }

        public decimal? TotalOrderDiscount { get; set; }
        public decimal? TotalOrderPrice { get; set; }
        public decimal? TotalOrderPaidPrice { get; set; }


        public OrderStates? State { get; set; }
        public string? PaymentType { get; set; }
        public CallRings? CallRing { get; set; }
        public decimal? DeliveryFee { get; set; }
        public string? DeliveryType { get; set; }



        public bool? IsCampaignApplied { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public int? CampaignId { get; set; }
        public decimal? CampaignDiscount { get; set; }


        public DateTime? OrderDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public List<OrderItemDto> OrdersItem { get; set; }

        public int AddressId { get; set; }
        [NotMapped]
        public Address Address { get; set; }


        public DateTime? PaymentApprovedDate { get; set; }// burdan sonra
        public int OrderContactId { get; set; }  
        public int BasketId { get; set; }

    }
}

