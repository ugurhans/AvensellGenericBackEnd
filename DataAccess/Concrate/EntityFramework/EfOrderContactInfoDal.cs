using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfOrderContactInfoDal : EfEntityRepositoryBase<OrderContactInfo, AvenSellContext>, IOrderContactInfoDal
    {

    }
}
