using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignSpecialDiscountDal : EfEntityRepositoryBase<CampaignSpecialDiscount, AvenSellContext>, ICampaignSpecialDiscountDal
    {
        public List<CampaignSpecialDiscount> GetAllDto(Expression<Func<CampaignSpecialDiscount, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignSpecialDiscounts
                           
                             join pS in context.Products
                             on c.ProductID equals pS.Id
                             select new CampaignSpecialDiscount()
                             {
                            
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,                
                                StartDate = c.StartDate,
                                MinPurchaseAmount = c.MinPurchaseAmount,
                                CampaignDetail = c.CampaignDetail,
                                CampaignImageUrl = c.CampaignImageUrl,
                                CampaignName = c.CampaignName,
                                ProductID = c.ProductID,
                                PromotionPeriodName = c.PromotionPeriodName,

                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
