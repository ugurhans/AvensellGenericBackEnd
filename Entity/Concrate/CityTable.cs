using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class CityTable
    {
        [Key]
        public short CityId { get; set; }
        public string CityName { get; set; }
    }
}
