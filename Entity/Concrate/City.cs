using Core.Entities;


namespace Entity.Entities
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlakaNo { get; set; }
        public int PhoneCode { get; set; }
        public int RowNumber { get; set; }
    }
}
