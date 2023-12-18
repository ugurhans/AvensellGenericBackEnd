using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate
{
    public class ResetPasswordCode:IEntity
    {
        public int UserId { get; set; }
        public int ExpirationDate { get; set; }
        public int Code { get; set; }
        
            
    }
}
