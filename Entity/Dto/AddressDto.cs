using System;
using Core;

namespace Entity.Dto
{
    public class AddressDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Header { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public int BuildingNo { get; set; }
        public int Floor { get; set; }
        public string ApartmentNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public string Phone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}

