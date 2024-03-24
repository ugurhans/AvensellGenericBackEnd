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
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}

