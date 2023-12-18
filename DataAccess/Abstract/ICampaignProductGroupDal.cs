using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICampaignProductGroupDal : IEntityRepository<CampaignProductGroup>
    {
        List<CampaignProductGroupDto> GetAllDto(Expression<Func<CampaignProductGroupDto, bool>> filter);
    }
}

