using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class FAQs : IEntity
    {
        public int Id { get; set; }
        public string? Desc { get; set; }
        public string? Label { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}

