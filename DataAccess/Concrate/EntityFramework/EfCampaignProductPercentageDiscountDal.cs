using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate.paytr;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignProductPercentageDiscountDal : EfEntityRepositoryBase<CampaignProductPercentageDiscount, AvenSellContext>, ICampaignProductPercentageDiscountDal
    {
        public List<CampaignProductPercentageDiscount> GetAllDto(Expression<Func<CampaignProductPercentageDiscount, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignProductPercentageDiscounts


                             join pG in context.Products
                             on c.CombinedProduct equals pG.Id.ToString()
                             select new CampaignProductPercentageDiscount()
                             {
                        
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,                 
                                 StartDate = c.StartDate,
                                 MinPurchaseAmount = c.MinPurchaseAmount,
                                 CampaignDetail = c.CampaignDetail, 
                                 CampaignImageUrl = c.CampaignImageUrl,
                                 CampaignName = c.CampaignName, 
                                 CombinedProduct= c.CombinedProduct,
                                 PercentageDiscountRate= c.PercentageDiscountRate,

                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
