using System;
using Core;
using Core.Entities;
using Entity.Concrate;

namespace Entity.Dto
{
    public class BasketCheckBoxTypeDto : IDto
    {
        public List<PaymentType> Payment { get; set; }
        public OnlinePayment OnlinePayment { get; set; }
        public EmptyDelivery EmptyDelivery { get; set; }
        public List<Delivery> Delivery { get; set; }
        public MarketVariable MarketVariables { get; set; }
    }

    public class PaymentType : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public bool IsActive { get; set; }
    }
    public class OnlinePayment : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Entegrator { get; set; }
        public bool IsActive { get; set; }

    }
    public class EmptyDelivery : IEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Value { get; set; }
    }
    public class Delivery : IEntity
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}

