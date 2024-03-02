using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrate.EntityFramework;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class MarketSettingManager : IMarketSettingService
    {
        private readonly IMarketSettingDal _marketSettingDal;

        public MarketSettingManager(IMarketSettingDal marketSettingDal)
        {
            _marketSettingDal = marketSettingDal;
        }

        public IResult Add(MarketSetting marketSetting)
        {
            _marketSettingDal.Add(marketSetting);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id)
        {
            _marketSettingDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<MarketSetting> GetById(int id)
        {
            return new SuccessDataResult<MarketSetting>(_marketSettingDal.Get(b => b.Id == id));
        }

        public IDataResult<List<MarketSetting>> GetAll()
        {
            return new SuccessDataResult<List<MarketSetting>>(_marketSettingDal.GetAll());
        }

        public IResult Update(MarketSetting marketSetting)
        {
            _marketSettingDal.Update(marketSetting);
            return new SuccessResult(Messages.Updated);
        }
    }
}
