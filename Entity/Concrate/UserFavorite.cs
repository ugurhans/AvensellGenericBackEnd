using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entity.Concrete
{
    public class UserFavorite : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
