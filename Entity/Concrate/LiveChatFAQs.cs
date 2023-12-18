using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class LiveChatFAQs : IEntity
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}

