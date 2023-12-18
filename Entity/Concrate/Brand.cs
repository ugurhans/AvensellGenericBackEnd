using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? Description { get; set; }
    }
}

