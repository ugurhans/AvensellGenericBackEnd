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
        private readonly IMarketSettingItemDal _marketSettingItemDal;

        public MarketSettingManager(IMarketSettingDal marketSettingDal, IMarketSettingItemDal marketSettingItemDal)
        {
            _marketSettingDal = marketSettingDal;
            _marketSettingItemDal = marketSettingItemDal;
        }

        public IResult Add(MarketSetting marketSetting)
        {
            try
            {
                if (marketSetting.DeliveryandPaymentOptions != null && marketSetting.DeliveryandPaymentOptions.Any())
                {
                    foreach (var item in marketSetting.DeliveryandPaymentOptions)
                    {
                        _marketSettingItemDal.Add(item);
                    }
                }

                _marketSettingDal.Add(marketSetting);

                return new SuccessResult(Messages.Added);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Error occurred while adding market setting: {ex.Message}");
            }
        }

        public IDataResult<MarketSetting> GetById(int id)
        {
            return _marketSettingDal.GetMarketSetting(id);
        }

        public IDataResult<List<MarketSetting>> GetAll()
        {
            return new SuccessDataResult<List<MarketSetting>>(_marketSettingDal.GetAllMarketSetting());
        }

        public IResult Delete(int id)
        {
            try
            {
                _marketSettingItemDal.Delete(id);

                _marketSettingDal.Delete(id);

                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Error occurred while deleting market setting: {ex.Message}");
            }
        }


        public IResult Update(MarketSetting marketSetting)
        {
            try
            {
                if (marketSetting.DeliveryandPaymentOptions != null && marketSetting.DeliveryandPaymentOptions.Any())
                {
                    foreach (var item in marketSetting.DeliveryandPaymentOptions)
                    {
                        _marketSettingItemDal.Update(item);
                    }
                }

                _marketSettingDal.Update(marketSetting);

                return new SuccessResult(Messages.Updated);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Error occurred while updating market setting: {ex.Message}");
            }
        }

    }
}
