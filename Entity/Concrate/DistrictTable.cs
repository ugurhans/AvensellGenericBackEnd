using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class DistrictTable
    {
        [Key]
        public short DistrictId { get; set; }
        public short CityId { get; set; }
        public string DistrictName { get; set; }
    }
}
