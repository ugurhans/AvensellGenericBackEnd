using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class MuhitTable
    {
        [Key]
        public short MuhitId { get; set; }
        public short DistrictId { get; set; }
        public string MuhitName { get; set; }
    }
}
