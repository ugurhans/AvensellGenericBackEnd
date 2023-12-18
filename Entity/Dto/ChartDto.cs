using Core;
using Core.Utilities.Results;
using Entity.Concrate.paytr;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class ChartDto : IDto
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }
}

