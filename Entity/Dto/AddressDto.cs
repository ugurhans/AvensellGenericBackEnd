using System;
using Core;

namespace Entity.Dto
{
    public class AddressDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public int MuhitId { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string NeighborhoodName { get; set; }
        public string MuhitName { get; set; }
        public string Header { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public string BuildingNo { get; set; }
        public string Floor { get; set; }
        public string ApartmentNo { get; set; }
        public string Phone { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string PostalCode { get; set; }
        public bool? IsActive { get; set; }
    }
}

