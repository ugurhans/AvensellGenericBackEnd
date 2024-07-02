using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entity.Concrete.Address;

namespace Entity.Concrate
{
    public class OrderContactInfo : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Header { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public string BuildingNo { get; set; }
        public string Floor { get; set; }
        public string ApartmentNo { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Muhit { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string PostalCode { get; set; }
        public bool? IsActive { get; set; }
        public int AddressesId { get; set; }
        public string FullAddress { get; set; }


    }
}
