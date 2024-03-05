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
    public class EfCampaignCombinedDiscountDal : EfEntityRepositoryBase<CampaignCombinedDiscount, AvenSellContext>, ICampaignCombinedDiscountDal
    {
        public List<CampaignCombinedDiscount> GetAllDto(Expression<Func<CampaignCombinedDiscount, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignCombinedDiscounts
                             
                             join pS in context.Products
                             on c.CombinedProduct equals pS.Id.ToString()
                             select new CampaignCombinedDiscount()
                             {
                          
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 MaxDiscountAmount = c.MaxDiscountAmount,
                                 CampaignDetail = c.CampaignDetail,
                                 CampaignImageUrl=c.CampaignImageUrl,
                                 CampaignName=c.CampaignName,
                                 CombinedProduct=c.CombinedProduct,
                                 PercentageDiscountRate=c.PercentageDiscountRate,
                                 StartDate = c.StartDate
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
