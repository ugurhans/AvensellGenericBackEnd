using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Neighborhood : IEntity
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string SemtName { get; set; }
        public string PostalCode { get; set; }
    }
}
