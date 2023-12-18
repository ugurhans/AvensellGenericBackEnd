using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using Entity.Concrete;
using Entity.Enum;

namespace Entity.Dto
{
    public class OrderSimpleDto : IDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public OrderStates? State { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Price { get; set; }
        public List<OrderItemDto> OrdersItem { get; set; }
    }
}

