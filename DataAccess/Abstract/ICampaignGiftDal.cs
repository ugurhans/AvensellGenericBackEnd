using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICampaignGiftDal : IEntityRepository<CampaignGift>
    {
        List<CampaignGiftDto> GetAllDto(Expression<Func<CampaignGiftDto, bool>> filter);

    }
}

