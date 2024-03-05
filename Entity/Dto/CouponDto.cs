using Core;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class CouponDto:IDto
    {
        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CouponDetail { get; set; }
        public string? Name { get; set; }
        public CouponTypes? CouponTypes { get; set; }
        public string? CouponImageUrl { get; set; }
    }
}
