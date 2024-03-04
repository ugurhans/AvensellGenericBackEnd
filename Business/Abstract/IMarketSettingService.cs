using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMarketSettingService
    {
        IDataResult<List<MarketSetting>> GetAll();
        IDataResult<MarketSetting> GetById(int id);
        IResult Update(MarketSetting marketSetting);
        IResult Add(MarketSetting marketSetting);
        IResult Delete(int id);

    }
}
