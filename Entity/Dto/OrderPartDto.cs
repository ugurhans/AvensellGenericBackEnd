using System;
using Core;

namespace Entity.Dto
{
    public class OrderPartDto : IDto
    {
        public List<OrderSimpleDto> UnApproved { get; set; }
        public List<OrderSimpleDto> WaitingOrders { get; set; }
        public List<OrderSimpleDto> CompletedOrders { get; set; }
        public List<OrderSimpleDto> CanceledOrders { get; set; }
    }
}

