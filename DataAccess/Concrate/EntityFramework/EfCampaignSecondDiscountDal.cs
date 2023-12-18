using System;
using Core.DataAccess.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignSecondDiscountDal : EfEntityRepositoryBase<CampaignSecondDiscount, AvenSellContext>, ICampaignSecondDiscountDal
    {
        public List<CampaignSecondDiscountDto> GetAllDto(Expression<Func<CampaignSecondDiscountDto, bool>> filter)

        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignSecondDiscounts
                             join pF in context.Products
                              on c.ProductFirstId equals pF.Id
                             select new CampaignSecondDiscountDto()
                             {
                                 ProductFirstName = pF.Name,
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 ProductFirstId = c.ProductFirstId,
                                 StartDate = c.StartDate,
                                 ProductSecondDiscount = c.ProductSecondDiscount
                             };
                return filter == null
                 ? result.ToList()
                 : result.Where(filter).ToList();
            }
        }
    }
}

