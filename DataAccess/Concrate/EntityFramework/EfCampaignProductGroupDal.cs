using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignProductGroupDal : EfEntityRepositoryBase<CampaignProductGroup, AvenSellContext>, ICampaignProductGroupDal
    {
        public List<CampaignProductGroupDto> GetAllDto(Expression<Func<CampaignProductGroupDto, bool>> filter)

        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignProductGroups
                             join pF in context.Products
                              on c.ProductFirstId equals pF.Id
                             join pS in context.Products
                             on c.ProductSecondId equals pS.Id
                             select new CampaignProductGroupDto()
                             {
                                 ProductFirstName = pF.Name,
                                 ProductSecondName = pS.Name,
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 ProductFirstId = c.ProductFirstId,
                                 ProductSecondId = c.ProductSecondId,
                                 StartDate = c.StartDate,
                                 ProductFirstDiscount = c.ProductFirstDiscount,
                                 ProductSecondDiscount = c.ProductSecondDiscount
                             };
                return filter == null
                   ? result.ToList()
                   : result.Where(filter).ToList();
            }
        }
    }
}

