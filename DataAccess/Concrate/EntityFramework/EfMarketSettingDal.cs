using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfMarketSettingDal : EfEntityRepositoryBase<MarketSetting, AvenSellContext>, IMarketSettingDal
    {






        public List<MarketSetting> GetAllMarketSetting()
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from setting in context.MarketSettings
                             join item in context.marketSettingItems
                             on setting.Id equals item.Id into items
                             select new MarketSetting
                             {
                                 Id = setting.Id,
                                 Background = setting.Background,
                                 CampaignEnabled = setting.CampaignEnabled,
                                 CouponEnabled = setting.CouponEnabled,
                                 DeliveryFee = setting.DeliveryFee,
                                 Secondary = setting.Secondary,
                                 DeliveryandPaymentOptions = items.ToList(),
                                 EndTime = setting.EndTime,
                                 LogoURL = setting.LogoURL,
                                 Mail = setting.Mail,
                                 MarketName = setting.MarketName,
                                 MinimumBasketPrice = setting.MinimumBasketPrice,
                                 PrimaryText = setting.PrimaryText,
                                 StartTime = setting.StartTime,
                                 Text = setting.Text,
                                 Warning = setting.Warning,
                                 Website = setting.Website,
                                 Whatsapp = setting.Whatsapp,
                                 Error = setting.Error
                             };

                return result.ToList();
            }
        }


        public IDataResult<MarketSetting> GetMarketSetting(int Id)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from setting in context.MarketSettings
                             join item in context.marketSettingItems
                             on setting.Id equals item.Id into items
                             where setting.Id == Id
                             select new MarketSetting
                             {
                                 Id = setting.Id,
                                 Background = setting.Background,
                                 CampaignEnabled = setting.CampaignEnabled,
                                 CouponEnabled = setting.CouponEnabled,
                                 DeliveryFee = setting.DeliveryFee,
                                 Secondary = setting.Secondary,
                                 DeliveryandPaymentOptions = items.ToList(),
                                 EndTime = setting.EndTime,
                                 LogoURL = setting.LogoURL,
                                 Mail = setting.Mail,
                                 MarketName = setting.MarketName,
                                 MinimumBasketPrice = setting.MinimumBasketPrice,
                                 PrimaryText = setting.PrimaryText,
                                 StartTime = setting.StartTime,
                                 Text = setting.Text,
                                 Warning = setting.Warning,
                                 Website = setting.Website,
                                 Whatsapp = setting.Whatsapp,
                                 Error = setting.Error
                             };

                var marketSetting = result.FirstOrDefault();
                if (marketSetting != null)
                {
                    return new SuccessDataResult<MarketSetting>(marketSetting);
                }
                else
                {
                    return new ErrorDataResult<MarketSetting>("Market setting not found");
                }
            }
        }



    }
}
