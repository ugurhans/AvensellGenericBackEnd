using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class FAQs : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string AddedDate { get; set; }
        public string EditDate { get; set; }
    }
}

