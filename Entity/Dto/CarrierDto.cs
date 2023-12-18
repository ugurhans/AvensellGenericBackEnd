using System;
using Core.Entities.Concrete;

namespace Entity.Dto
{
    public class CarrierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
    }
}

