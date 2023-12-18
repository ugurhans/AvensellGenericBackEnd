using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICampaignSecondDiscountDal : IEntityRepository<CampaignSecondDiscount>
    {
        List<CampaignSecondDiscountDto> GetAllDto(Expression<Func<CampaignSecondDiscountDto, bool>> filter);
    }
}

