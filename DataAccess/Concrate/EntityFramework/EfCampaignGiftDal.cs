using System;
using Core.DataAccess.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignGiftDal : EfEntityRepositoryBase<CampaignGift, AvenSellContext>, ICampaignGiftDal
    {
        public List<CampaignGiftDto> GetAllDto(Expression<Func<CampaignGiftDto, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignGifts
                             join pF in context.Products
                              on c.ProductFirstId equals pF.Id
                             join pS in context.Products
                             on c.ProductSecondId equals pS.Id
                             join pG in context.Products
                             on c.ProductGift equals pG.Id
                             select new CampaignGiftDto()
                             {
                                 ProductFirstName = pF.Name,
                                 ProductSecondName = pS.Name,
                                 ProductGiftName = pG.Name,
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 ProductFirstId = c.ProductFirstId,
                                 ProductSecondId = c.ProductSecondId,
                                 ProductGiftId = c.ProductGift,
                                 StartDate = c.StartDate
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}

