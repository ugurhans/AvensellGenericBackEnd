using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate.paytr
{
    public class PaytrLog:IEntity
    {

        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public int? OrderId { get; set; }
        public int? UserId { get; set; }
        public bool? Success { get; set; }
        public ErrorTypes? ErrorType { get; set; }
        public string? ContentMessage { get; set; }
    }

    public enum ErrorTypes
    {
        Order_Not_Found,
        User_Not_Found,
        PayTr_Error,
        InternalError,
    }
}
