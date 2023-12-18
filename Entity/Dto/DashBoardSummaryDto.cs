using System;
using Core;
using Core.Entities;

namespace Entity.Dto
{
    public class DashBoardSummaryDto : IDto
    {
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
        public int ProductCount { get; set; }
        public int AvenCoinCount { get; set; }
    }
}

