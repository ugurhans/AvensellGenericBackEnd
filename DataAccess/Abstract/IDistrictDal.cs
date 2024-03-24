using Core.DataAccess.EntityFramework;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;

namespace DataAccess.Abstract
{
    public interface IDistrictDal : IEntityRepository<District>
    {
    }
}
