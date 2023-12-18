using System;
using Core;

namespace Entity.Dto
{
    public class BasketAddItemDto : IDto
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}

