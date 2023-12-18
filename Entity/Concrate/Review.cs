using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}

