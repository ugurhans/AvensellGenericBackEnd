using System;
using Core;

namespace Entity.Dto
{
    public class OrderItemDto : IDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? BasketItemId { get; set; }


        public decimal? TotalPrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalPaidPrice { get; set; }


        public int? ProductCount { get; set; }
        public string? ProductName { get; set; }

        public decimal? ProductPrice { get; set; }
        public decimal? ProductDiscountPrice { get; set; }
        public decimal ProductPaidPrice { get; set; }


        public string? Image { get; set; }

    }

    public class OrderItemDtoBasic
    {
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
    }
}

