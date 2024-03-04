using Core.DataAccess;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMarketSettingDal : IEntityRepository<MarketSetting>
    {
        public IDataResult<MarketSetting> GetMarketSetting(int Id);
        public List<MarketSetting> GetAllMarketSetting();
    }
}
