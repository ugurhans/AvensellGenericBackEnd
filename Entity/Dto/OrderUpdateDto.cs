using Core;
using Entity.Concrate;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class OrderUpdateDto:IDto
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BasketId { get; set; }

        public decimal? TotalOrderDiscount { get; set; }
        public decimal? TotalOrderPrice { get; set; }
        public decimal? TotalOrderPaidPrice { get; set; }


        public DateTime? OrderDate { get; set; }
        public OrderStates? State { get; set; }

        public DateTime? ConfirmDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int AddressId { get; set; }




    }
}
