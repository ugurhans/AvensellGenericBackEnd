﻿using Core;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class CouponAddDto:IDto
    {
        public string Code { get; set; }
        public decimal Discount { get; set; } //indirim miktarı
        public int MinBasketCost { get; set; } //min sepet tutarı
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CouponTypes? couponTypes { get; set; }
        public string CouponImageUrl { get; set; }
        public string CouponName { get; set; }
        public string CombinedProduct { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

    }
}
