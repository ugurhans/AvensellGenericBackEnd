using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Abstract
{
    public interface ICampaignSecondDiscountDal : IEntityRepository<CampaignSecondDiscount>
    {
        List<CampaignSecondDiscountDto> GetAllDto(Expression<Func<CampaignSecondDiscountDto, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignSecondDiscounts.RemoveRange(context.CampaignSecondDiscounts.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}

