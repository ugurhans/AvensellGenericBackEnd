using Core.Entities;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class OrderRepeat:IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime CleaningDate { get; set; }
        public int RepeatOrder { get; set; }
        public OrderStates Status { get; set; }
    }
}
