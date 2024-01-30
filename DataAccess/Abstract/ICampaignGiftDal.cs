using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Abstract
{
    public interface ICampaignGiftDal : IEntityRepository<CampaignGift>
    {
        List<CampaignGiftDto> GetAllDto(Expression<Func<CampaignGiftDto, bool>> filter);

        public void DeleteRange(int Id)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignGifts.RemoveRange(context.CampaignGifts.Where(a => a.Id == Id));
            context.SaveChanges();

        }

    }
}

