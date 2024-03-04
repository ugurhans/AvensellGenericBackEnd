using Core.DataAccess;
using Entity.Concrate;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMarketSettingItemDal : IEntityRepository<MarketSettingItem>
    {
    }
}
