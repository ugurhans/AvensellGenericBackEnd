using Core;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Entity.Dto
{
    public class OrderCreateRequestDto: IDto
    {

        public int UserId { get; set; }

        public decimal? TotalOrderDiscount { get; set; }
        public decimal? TotalOrderPrice { get; set; }
        public decimal? TotalOrderPaidPrice { get; set; }


        public OrderStates? State { get; set; }
        public CallRings? CallRing { get; set; }
      //  public decimal? DeliveryFee { get; set; }
        public string MarketName { get; set; } //bu shops tablosundaki name ile aynı olursa oradaki deliveryfee degerini alıyoruz..



        public bool? IsCampaignApplied { get; set; }
        public CampaignTypes? CampaignType { get; set; }
        public int? CampaignId { get; set; }
        public decimal? CampaignDiscount { get; set; }
        public int AddressId { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? CompletedDate { get; set; }

       // public List<OrderItemDto> OrdersItem { get; set; }


        public DateTime? PaymentApprovedDate { get; set; }// burdan sonra
        public int OrderContactId { get; set; }
        public int BasketId { get; set; }
        public PaymentTypes? PaymentType { get; set; }




    }
}
