using System;
using Core;

namespace Entity.Dto
{
    public class GraphPieDto : IDto
    {
        public List<string> Labels { get; set; }
        public List<int?> Data { get; set; }

    }
}

