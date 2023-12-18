using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class MarketVariable : IEntity
    {
        public int Id { get; set; }
        public string MarketOpeningHour { get; set; }
        public string MarketClosingHour { get; set; }
        public decimal MinBoxCost { get; set; }
        public decimal DeliveryFee { get; set; }
    }
}

