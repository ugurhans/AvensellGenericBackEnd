using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class Thema : IEntity
    {
        public int Id { get; set; }
        public string? PrimaryText { get; set; }
        public string? Secondary { get; set; }
        public string? Text { get; set; }
        public string? Background { get; set; }
        public string? Error { get; set; }
        public string? Warning { get; set; }

    }
}


