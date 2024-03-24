using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class District : IEntity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
    }
}
