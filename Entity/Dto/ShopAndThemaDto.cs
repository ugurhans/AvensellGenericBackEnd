using Core;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class ShopAndThemaDto:IDto
    {
        public Shop Shop { get; set; }
   
        public Thema Colors { get; set; }
    }

}
