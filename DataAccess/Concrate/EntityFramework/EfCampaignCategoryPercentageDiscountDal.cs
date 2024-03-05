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

    public class EfCampaignCategoryPercentageDiscountDal : EfEntityRepositoryBase<CampaignCategoryPercentageDiscount, AvenSellContext>, ICampaignCategoryPercentageDiscountDal
    {
        public List<CampaignCategoryPercentageDiscount> GetAllDto(Expression<Func<CampaignCategoryPercentageDiscount, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignCategoryPercentageDiscounts
                             join pF in context.Products
                          //    on c.CategoryId equals pF.CategoryId
                          on c.ProductId equals pF.Id
                             select new CampaignCategoryPercentageDiscount()
                             {
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 StartDate = c.StartDate,
                                 MinPurchaseAmount = c.MinPurchaseAmount,
                                 CampaignDetail= c.CampaignDetail,
                                 CampaignImageUrl= c.CampaignImageUrl,
                                 CampaignName= c.CampaignName,
                                 CategoryId= c.CategoryId,
                                 PercentageDiscountRate= c.PercentageDiscountRate, 
                             };
                return filter == null

                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
