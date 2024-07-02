using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete
{
    public class NeighbourhoodTable : IEntity
    {
        [Key]
        public int NeighbourhoodId { get; set; }
        public short MuhitId { get; set; }
        public string NeighbourhoodName { get; set; }
        public short pk_id { get; set; }
    }
}
