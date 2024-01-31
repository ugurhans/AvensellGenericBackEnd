using Core;
using Entity.Concrete;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class OrderBasicDto:IDto
    {
        public int OrderId { get; set; }
        public decimal? TotalOrderPaidPrice { get; set; }
        public OrderStates? State { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<OrderItemDtoBasic> OrdersItem { get; set; }

    }
}
